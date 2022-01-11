using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewel
{
    internal class Cloudinary
    {
        private static Cloudinary cloudinary;
        private const string CLOUD_NAME = "dvjruizqp";
        private const string API_KEY = "339428134957487";
        private const string API_SECRET = "I-hxE1ZdHCtrEbSFJnDHj9OID0o";
        private Account account;
        public Cloudinary()
        {
            cloudinary = new Cloudinary();
            account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
        }
       

    }
}
