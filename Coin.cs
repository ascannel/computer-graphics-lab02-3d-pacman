using PacMan.Graphics;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace PacMan
{

    internal class Coin
    {
        public List<Vector3> gameVerts;
        public List<Vector2> gameGraphic = new List<Vector2>();
        public List<uint> gameInd;

        VAO gameVAO;
        VBO gameVertexVBO;
        VBO gameGraphicVBO;
        IBO gameIBO;

        Texture texture;

        public Coin()
        {
            GenerateCoinMesh(0.014f, out gameVerts, out gameInd);

            for (int i = 0; i < gameVerts.Count; i++)
            {
                Vector3 n = gameVerts[i];
                n.Normalize();
                float u = (float)(MathHelper.Atan2(n.X, n.Z) / (2 * MathHelper.Pi) + 0.5f);
                float v = n.Y * 0.5f + 0.5f;
                gameGraphic.Add(new Vector2(u, v));
            }
            BuildObject("coin.png");
        }

        private void GenerateCoinMesh(float scale, out List<Vector3> vertices, out List<uint> indices)
        {
            vertices = new List<Vector3>();
            indices = new List<uint>();

            int slices = 22;
            int stacks = 10;

            for (int i = 0; i <= stacks; i++)
            {
                float v = (float)i / stacks;
                float phi = v * (float)Math.PI;

                for (int j = 0; j <= slices; j++)
                {
                    float u = (float)j / slices;
                    float theta = u * 2f * (float)Math.PI;

                    float x = (float)(Math.Cos(theta) * Math.Sin(phi));
                    float y = (float)Math.Cos(phi);
                    float z = (float)(Math.Sin(theta) * Math.Sin(phi));

                    vertices.Add(new Vector3(x, y, z) * scale);
                }
            }

            for (int i = 0; i < stacks; i++)
            {
                for (int j = 0; j < slices; j++)
                {
                    int first = (i * (slices + 1)) + j;
                    int second = first + slices + 1;

                    indices.Add((uint)first);
                    indices.Add((uint)second);
                    indices.Add((uint)(first + 1));

                    indices.Add((uint)second);
                    indices.Add((uint)(second + 1));
                    indices.Add((uint)(first + 1));
                }
            }
        }

        public void BuildObject(String path)
        {
            gameVAO = new VAO();
            gameVAO.Bind();

            gameVertexVBO = new VBO(gameVerts);
            gameVertexVBO.Bind();
            gameVAO.LinkToVAO(0, 3, gameVertexVBO);

            gameGraphicVBO = new VBO(gameGraphic);
            gameGraphicVBO.Bind();
            gameVAO.LinkToVAO(1, 2, gameGraphicVBO);

            gameIBO = new IBO(gameInd);

            texture = new Texture(path);
        }

        public void Render(ShaderProgram program)
        {
            program.Bind();
            gameVAO.Bind();
            gameIBO.Bind();
            texture.Bind();
            GL.DrawElements(PrimitiveType.Triangles, gameInd.Count, DrawElementsType.UnsignedInt, 0);
        }

        public void Delete()
        {
            gameVAO.Delete();
            gameVertexVBO.Delete();
            gameGraphicVBO.Delete();
            gameIBO.Delete();
            texture.Delete();
        }

    }
}

