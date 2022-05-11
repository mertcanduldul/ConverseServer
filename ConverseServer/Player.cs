﻿using System;
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

            inputs = new bool[4];
        }

        public void Update()
        {
            Vector2 inputDirection = Vector2.Zero;
            if (inputs[0])
            {
                inputDirection.Y += 1;
            }
            if (inputs[1])
            {
                inputDirection.Y -= 1;
            }
            if (inputs[2])
            {
                inputDirection.X += 1;
            }
            if (inputs[3])
            {
                inputDirection.X -= 1;
            }
            Move(inputDirection);
        }

        private void Move(Vector2 _inputDirection)
        {
            Vector3 forward = Vector3.Transform(new Vector3(0, 0, 1), rotation);
            Vector3 right = Vector3.Normalize(Vector3.Cross(forward, new Vector3(0, 1, 0)));

            Vector3 moveDirection = right * _inputDirection.X + forward * _inputDirection.Y;

            //position += moveDirection * moveSpeed;

            ServerSend.PlayerPosition(this);
            ServerSend.PlayerRotation(this);
        }



        public void SetInputs(bool[] _inputs, Quaternion _rotation,Vector3 _position,int _animatorState)
        {
            inputs = _inputs;
            rotation = _rotation;
            position = _position;
            animatorState = _animatorState;
        }
    }
}
