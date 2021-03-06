using AxTools.WoW.Internals;
using System;

namespace AxTools.WoW.Helpers
{
    public class ChatMsg : IEquatable<ChatMsg>
    {
        public static readonly ChatMsg Empty = new ChatMsg(WoWChatMsgType.Addon, 0, string.Empty, WoWGUID.Zero, 0, string.Empty);
        public readonly WoWChatMsgType Type;
        public byte Channel;
        public string Sender;
        public readonly WoWGUID SenderGUID;
        public string Text;
        public readonly int TimeStamp;

        internal ChatMsg(WoWChatMsgType type, byte channel, string sender, WoWGUID senderGUID, int timestamp, string text)
        {
            Type = type;
            Channel = channel;
            Sender = sender;
            SenderGUID = senderGUID;
            TimeStamp = timestamp;
            Text = text;
        }

        public bool Equals(ChatMsg other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other is null)
            {
                return false;
            }
            return Type == other.Type && SenderGUID == other.SenderGUID && TimeStamp == other.TimeStamp;
        }

        public override bool Equals(object other)
        {
            return Equals(other as ChatMsg);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 1363908689;
                hashCode = hashCode * -1521134295 + Type.GetHashCode();
                hashCode = hashCode * -1521134295 + SenderGUID.GetHashCode();
                hashCode = hashCode * -1521134295 + TimeStamp.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(ChatMsg a, ChatMsg b)
        {
            if (a is null)
            {
                return b is null;
            }
            return a.Equals(b);
        }

        public static bool operator !=(ChatMsg a, ChatMsg b)
        {
            return !(a == b);
        }

        public DateTime GetTimestampAsDateTime()
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(TimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}