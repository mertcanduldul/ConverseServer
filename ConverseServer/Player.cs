using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Server
{
    class Player
    {
        public int id;
        public string username;

        public Vector3 position;
        public Quaternion rotation;

        private float moveSpeed = 5f / Constants.TICKS_PER_SEC;
        private bool[] inputs;

        public int animatorState;

        public Player(int _id, string _username, Vector3 _spawnPosition)
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.Identity;

        }

        public void Update()
        {
            Move();
        }

        private void Move()
        {
            ServerSend.PlayerPosition(this);
            ServerSend.PlayerRotation(this);
        }



        public void SetMovements(Vector3 _position, Quaternion _rotation, int _animatorState)
        {
            rotation = _rotation;
            position = _position;
            animatorState = _animatorState;
        }
    }
}
