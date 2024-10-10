namespace PetShopClient.Helper
{
    public class SaveImage
    {
        public static string? SaveUploadedFile(IFormFile file, string uploadPath, string existingFileName = null)
        {
            if (file != null && file.Length > 0)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                string newFileName;
                if (!string.IsNullOrEmpty(existingFileName))
                {
                    newFileName = existingFileName;
                }
                else
                {
                    newFileName = Guid.NewGuid().ToString() + fileExtension;
                }
                string fullPath = Path.Combine(uploadPath, newFileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return newFileName;
            }
            return null;
        }
    }
}
