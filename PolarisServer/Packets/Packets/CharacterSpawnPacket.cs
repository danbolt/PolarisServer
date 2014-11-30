﻿using System;

namespace PolarisServer.Packets
{
    public class CharacterSpawnPacket : Packet
    {
        public CharacterSpawnPacket(Models.Character character)
        {
            _character = character;

            _position.a = 0.000031f;
            _position.b = 1.0f;
            _position.c = 0.000031f;
            _position.d = -0.000031f;
            _position.e = -0.417969f;
            _position.f = 0.000031f;
            _position.g = 134.375f;
        }

        private Models.Character _character = null;
        private Models.MysteryPositions _position;

        #region implemented abstract members of Packet
        public override byte[] Build()
        {
            var writer = new PacketWriter();

            // Player header
            writer.WritePlayerHeader((uint)_character.Player.PlayerID);

            // Spawn position
            writer.Write(_position);

            writer.Write((ushort)0); // padding?
            writer.WriteFixedLengthASCII("Character", 32);
            writer.Write((ushort)1); // 0x44
            writer.Write((ushort)0); // 0x46
            writer.Write((uint)602); // 0x48
            writer.Write((uint)1); // 0x4C
            writer.Write((uint)53); // 0x50
            writer.Write((uint)0); // 0x54
            writer.Write((uint)47); // 0x58
            writer.Write((ushort)559); // 0x5C
            writer.Write((ushort)306); // 0x5E
            writer.Write((uint)_character.Player.PlayerID); // player ID copy
            writer.Write((uint)0); // "char array ugggghhhhh" according to PolarisLegacy
            writer.Write((uint)0); // "voiceParam_unknown4"
            writer.Write((uint)0); // "voiceParam_unknown8"
            writer.WriteFixedLengthUTF16(_character.Name, 16);
            writer.Write((uint)0); // 0x90
            writer.WriteStruct(_character.Looks);
            writer.WriteStruct(_character.Jobs);
            writer.WriteFixedLengthUTF16("", 32); // title?
            writer.Write((uint)0); // 0x204
            writer.Write((uint)0); // gmflag?
            writer.WriteFixedLengthUTF16(_character.Player.Nickname, 16); // nickname, maybe not 16 chars?
            for (int i = 0; i < 64; i++)
                writer.Write((byte)0);

            return writer.ToArray();
        }
        public override PolarisServer.Models.PacketHeader GetHeader()
        {
            return new Models.PacketHeader(8, 4);
        }
        #endregion
    }
}


