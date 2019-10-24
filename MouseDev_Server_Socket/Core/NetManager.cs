namespace MouseDev_Server_Socket.Core
{
    public static class NetManager
    {
        /// <summary>
        /// Server Port
        /// </summary>
        public const int PORT = 1618;
        /// <summary>
        /// Max Buffer Length
        /// </summary>
        public const short BUFFER_LENGTH = 128 * 4;
        /// <summary>
        /// Max Clients in room
        /// </summary>
        public const short MAX_CLIENTS_PER_ROOM = 30;
        /// <summary>
        /// RT Packet OPCODE
        /// </summary>
        public const byte RT_PACKET_OPCODE0 = 0;
        /// <summary>
        /// RT Packet OPCODE
        /// </summary>
        public const byte RT_PACKET_OPCODE1 = 1;
    }
}
