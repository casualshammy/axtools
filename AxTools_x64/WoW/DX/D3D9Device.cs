﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AxTools.WoW.DX
{
    // ReSharper disable InconsistentNaming
    internal sealed class D3D9Device : D3DDevice
    {
        private const int D3D9SdkVersion = 0x20;
        private const int D3DCREATE_SOFTWARE_VERTEXPROCESSING = 0x20;

        private VTableFuncDelegate _d3DDeviceRelease;
        private VTableFuncDelegate _d3DRelease;

        private IntPtr _pD3D;

        internal D3D9Device(Process targetProc)
            : base(targetProc, "d3d9.dll")
        {
        }

        internal override int BeginSceneVtableIndex
        {
            get { return VTableIndexes.Direct3DDevice9BeginScene; }
        }

        internal override int EndSceneVtableIndex
        {
            get { return VTableIndexes.Direct3DDevice9EndScene; }
        }

        internal override int PresentVtableIndex
        {
            get { return VTableIndexes.Direct3DDevice9Present; }
        }

        protected override void InitD3D(out IntPtr d3DDevicePtr)
        {
            _pD3D = Direct3DCreate9(D3D9SdkVersion);
            if (_pD3D == IntPtr.Zero)
                throw new Exception("Failed to create D3D.");

            var parameters = new D3DPresentParameters
                                 {
                                     Windowed = true,
                                     SwapEffect = 1,
                                     BackBufferFormat = 0
                                 };

            IntPtr createDevicePtr = GetVTableFuncAddress(_pD3D, VTableIndexes.Direct3D9CreateDevice);
            var createDevice = GetDelegate<CreateDeviceDelegate>(createDevicePtr);

            if (
                createDevice(_pD3D, 0, 1, Form.Handle, D3DCREATE_SOFTWARE_VERTEXPROCESSING, ref parameters,
                             out d3DDevicePtr) < 0)
            {
                throw new Exception("Failed to create device.");
            }
            _d3DDeviceRelease =
                GetDelegate<VTableFuncDelegate>(GetVTableFuncAddress(D3DDevicePtr, VTableIndexes.Direct3DDevice9Release));
            _d3DRelease = GetDelegate<VTableFuncDelegate>(GetVTableFuncAddress(_pD3D, VTableIndexes.Direct3D9Release));
        }

        protected override void CleanD3D()
        {
            if (D3DDevicePtr != IntPtr.Zero)
                _d3DDeviceRelease(D3DDevicePtr);

            if (_pD3D != IntPtr.Zero)
                _d3DRelease(_pD3D);
        }

        [DllImport("d3d9.dll")]
        internal static extern IntPtr Direct3DCreate9(uint sdkVersion);

        #region Embedded Types

        #region Nested type: CreateDeviceDelegate

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int CreateDeviceDelegate(
            IntPtr instance,
            uint adapter,
            uint deviceType,
            IntPtr focusWindow,
            uint behaviorFlags,
            [In] ref D3DPresentParameters presentationParameters,
            out IntPtr returnedDeviceInterface);

        #endregion Nested type: CreateDeviceDelegate

        #region Nested type: D3DPresentParameters

        [StructLayout(LayoutKind.Sequential)]
        internal struct D3DPresentParameters
        {
            internal readonly uint BackBufferWidth;
            internal readonly uint BackBufferHeight;
            internal uint BackBufferFormat;
            internal readonly uint BackBufferCount;
            internal readonly uint MultiSampleType;
            internal readonly uint MultiSampleQuality;
            internal uint SwapEffect;
            internal readonly IntPtr hDeviceWindow;

            [MarshalAs(UnmanagedType.Bool)]
            internal bool Windowed;

            [MarshalAs(UnmanagedType.Bool)]
            internal readonly bool EnableAutoDepthStencil;

            internal readonly uint AutoDepthStencilFormat;
            internal readonly uint Flags;
            internal readonly uint FullScreen_RefreshRateInHz;
            internal readonly uint PresentationInterval;
        }

        #endregion Nested type: D3DPresentParameters

        #region Nested type: VTableIndexes

        internal struct VTableIndexes
        {
            internal const int Direct3D9Release = 2;
            internal const int Direct3D9CreateDevice = 0x10;

            internal const int Direct3DDevice9Release = 2;
            internal const int Direct3DDevice9Reset = 0x10;
            internal const int Direct3DDevice9Present = 0x11;
            internal const int Direct3DDevice9BeginScene = 0x29;
            internal const int Direct3DDevice9EndScene = 0x2A;
            internal const int Direct3DDevice9Clear = 0x2B;
        }

        #endregion Nested type: VTableIndexes

        #endregion Embedded Types
    }

    // ReSharper restore InconsistentNaming
}