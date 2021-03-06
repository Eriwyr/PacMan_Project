﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PacMan_CHABRIER_REGNARD
{
    class AnimatedObject
    {

        // Class for every object displayed


        private Texture2D texture; //We store the texture
        private Vector2 position; //The position to display
        private Vector2 size; //The size of the image


        //Getters and setters
       public Texture2D getTexture()
        {
            return texture;
        }

        public void setTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public Vector2 getPos()
        {
            return position;
        }

        public void setPosition(Vector2 position)
        {
            this.position = position;
        }

        public Vector2 getSize()
        {
            return size;
        }

        public void setSize(Vector2 size)
        {
            this.size = size;
        }

        public AnimatedObject(Texture2D texture, Vector2 position, Vector2 size)
        {
            if (texture == null)
                throw new NullReferenceException();
            this.texture = texture;
            this.position = position;
            this.size = size;
        }

    }
}
