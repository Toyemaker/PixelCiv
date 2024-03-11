using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Library
{
    public class HexTile
    {
        private Texture2D _texture;
        private Vector2 _position;

        private Rectangle _sourceRect;
        private Color _color;
        private float _rotation;
        private Vector2 _origin;
        private Vector2 _scale;
        private SpriteEffects _effects;
        private float _layerDepth;

        private Polygon _boundingBox;

        public HexTile()
        {
            _texture = GameData.BaseTileTexture;
            _position = new Vector2();
            _sourceRect = _texture.Bounds;
            _color = Color.White;
            _rotation = 0;
            _origin = new Vector2();
            _scale = new Vector2(2);
            _effects = SpriteEffects.None;
            _layerDepth = 0;

            List<Vertex> vertices = new List<Vertex>()
            {
                new Vertex(new Vector2(0, 6)),
                new Vertex(new Vector2(0, 9)),
                new Vertex(new Vector2(4, 13)),
                new Vertex(new Vector2(11, 13)),
                new Vertex(new Vector2(15, 9)),
                new Vertex(new Vector2(15, 6)),
                new Vertex(new Vector2(11, 2)),
                new Vertex(new Vector2(4, 2)),
            };

            _boundingBox = new Polygon(vertices);
        }

        public Vector2 GetPosition()
        {
            return _position;
        }

        public HexTile SetPosition(Vector2 value)
        {
            _position = value;

            _boundingBox.SetPosition(value);

            return this;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_texture, _position * _scale, _sourceRect, _color, _rotation, _origin, _scale, _effects, _layerDepth);
        }

        public void Update(GameTime gameTime)
        {

        }

        public bool ContainsPoint(Vector2 point)
        {
            if (_boundingBox.ContainsPoint(point))
            {
                _color = Color.Red;

                return true;
            }

            return false;
        }
    }
}
