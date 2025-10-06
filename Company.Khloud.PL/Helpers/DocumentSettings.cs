namespace Company.Khloud.PL.Helpers
{
    public static class DocumentSettings
    {
        // 1.Upload
        // string : ImageName

        public static string UploadFile (IFormFile file ,string folderName )
        {
            //1. Get Folder Location
            //string folderPath = "C:\\Users\\HP\\Desktop\\MVC\\Company.Khloud\\Company.Khloud.PL\\wwwroot\\Files\\ "+ folderName; //Static

            //var folderPath =  Directory.GetCurrentDirectory() + "\\wwwroot\\Files\\" + folderName; //Dynamic

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", folderName);

            // 2.Get FileName And Make It Unique

            var fileName = $"{Guid.NewGuid()} {file.FileName}";

            //File Path : location + fileName

            var filePath = Path.Combine(folderPath, fileName);

            var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
        }

        // 2.Delete

        public static void  DeleteFile (string folderName , string fileName )
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", folderName, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
