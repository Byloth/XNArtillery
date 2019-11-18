using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.CompilerServices;

namespace ByloEngine.Net
{
    public enum PacketTypes
    {
        OK,
        IMLookingForMatches,
        IMHostingAMatch,
        IWantToJoin,
        IAcceptedYou,
        IDeclineYou,
        HereYouAre,
        IShot,
        IMAlive
    }

    [Serializable()]
    public class Packet : ISerializable
    {
        public PacketTypes type;
        public object content;

        public Packet()
        {
        }

        public Packet(PacketTypes packetType)
        {
            type = packetType;
        }

        public Packet(PacketTypes packetType, object packetContent)
        {
            type = packetType;
            content = packetContent;
        }

        public Packet(SerializationInfo info, StreamingContext context)
        {
            type = (PacketTypes)info.GetValue("PacketType", typeof(PacketTypes));
            content = (object)info.GetValue("PacketContent", typeof(object));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("PacketType", type);
            info.AddValue("PacketContent", content);
        }

        public byte[] Serialize()
        {
            BinaryFormatter encoder = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();

            encoder.Serialize(stream, this);

            return stream.ToArray();
        }

        public static Packet Deserialize(byte[] bytes)
        {
            BinaryFormatter decoder = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            Packet packet = new Packet();

            stream.Write(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);

            packet = (Packet)decoder.Deserialize(stream);

            return packet;
        }
    }
}
