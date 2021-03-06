﻿using Newtonsoft.Json;
using System;

namespace AxTools.Helpers
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class VersionExt
    {
        internal VersionExt(Version version)
        {
            Major = version.Major;
            Minor = version.Minor;
            Build = version.Build;
        }

        [JsonConstructor]
        internal VersionExt(int major, int minor, int build)
        {
            Major = major;
            Minor = minor;
            Build = build;
        }

        [JsonProperty(Order = 0, PropertyName = "Major")]
        internal readonly int Major;

        [JsonProperty(Order = 1, PropertyName = "Minor")]
        internal readonly int Minor;

        [JsonProperty(Order = 2, PropertyName = "Build")]
        internal readonly int Build;

        public static bool operator ==(VersionExt a, VersionExt b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }
            if (a is null || b is null)
            {
                return false;
            }
            return a.Major == b.Major && a.Minor == b.Minor && a.Build == b.Build;
        }

        public static bool operator !=(VersionExt a, VersionExt b)
        {
            return !(a == b);
        }

        public static bool operator >(VersionExt a, VersionExt b)
        {
            if (a.Major < b.Major)
            {
                return false;
            }
            if (a.Major > b.Major)
            {
                return true;
            }
            if (a.Minor < b.Minor)
            {
                return false;
            }
            if (a.Minor > b.Minor)
            {
                return true;
            }
            if (a.Build < b.Build)
            {
                return false;
            }
            if (a.Build > b.Build)
            {
                return true;
            }
            return false;
        }

        public static bool operator <(VersionExt a, VersionExt b)
        {
            if (a.Major < b.Major)
            {
                return true;
            }
            if (a.Major > b.Major)
            {
                return false;
            }
            if (a.Minor < b.Minor)
            {
                return true;
            }
            if (a.Minor > b.Minor)
            {
                return false;
            }
            if (a.Build < b.Build)
            {
                return true;
            }
            if (a.Build > b.Build)
            {
                return false;
            }
            return false;
        }

        public static bool operator <=(VersionExt a, VersionExt b)
        {
            return a < b || a == b;
        }

        public static bool operator >=(VersionExt a, VersionExt b)
        {
            return a > b || a == b;
        }

        protected bool Equals(VersionExt other)
        {
            return Major == other.Major && Minor == other.Minor && Build == other.Build;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((VersionExt)obj);
        }

        public override int GetHashCode()
        {
            return Major ^ Minor ^ Build;
        }

        public override string ToString()
        {
            return Major + "." + Minor + "." + Build;
        }
    }
}