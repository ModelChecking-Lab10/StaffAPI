using Microsoft.AspNetCore.Mvc;

namespace StaffAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Đường dẫn đến thư mục wwwroot
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            // Đường dẫn đến thư mục uploads (ví dụ: wwwroot/uploads)
            string uploadPath = Path.Combine(wwwRootPath, "uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Tạo tên file duy nhất để tránh trùng lặp
            string fileExtension = Path.GetExtension(file.FileName);
            string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

            // Đường dẫn lưu file vật lý
            string filePath = Path.Combine(uploadPath, uniqueFileName);

            try
            {
                // Lưu file vào server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                // Trả về đường dẫn tương đối (relative path)
                // Đây chính là đường dẫn bạn sẽ lưu vào cột Photo VARCHAR(300)
                string relativePath = Path.Combine("uploads", uniqueFileName).Replace("\\", "/");

                // Trả về { "path": "uploads/ten-file-duy-nhat.jpg" }
                return Ok(new { path = relativePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}