using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Entities;
using System.Data.Objects;

namespace Blog.Repository
{
    public class EFPictureRepository : IPictureRepository
    {
        private readonly string _connectionString;

        public EFPictureRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }


        public int AddPicture(byte[] data, string mimetype, string src)
        {
            int imgId = 0;

            using (ObjectContext context = new ObjectContext(this._connectionString))
            {
                var images = context.CreateObjectSet<Picture>();

                if (!images.Any(x => x.FileData == data))
                {
                    int maxId = images.Any() ? images.Max(x => x.Id) : 1;
                    Picture newImage = new Picture() { Id = +maxId, FileData = data, ImageMimeType = mimetype, Src = src };
                    imgId = newImage.Id;
                    images.AddObject(newImage);
                    context.SaveChanges();
                }
                else
                {
                    imgId = images.Single(x => x.FileData == data).Id;
                }
            }

            return imgId;
        }


        public Picture GetPicture(int id)
        {
            using (ObjectContext context = new ObjectContext(this._connectionString))
            {
                return context.CreateObjectSet<Picture>().Single(x=>x.Id == id);
            }
        }

        public Picture GetPicture(string src)
        {
            using (ObjectContext context = new ObjectContext(this._connectionString))
            {
                return context.CreateObjectSet<Picture>().Single(x=>x.Src == src);
            }
        }
    }
}
