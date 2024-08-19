﻿using CA_Final_Regia.Attributes;

namespace CA_Final_Regia.DTOs
{
    public class PictureDto
    {
        [AllowedExtension([".jpg", ".png"])]
        public IFormFile Image { get; set; }
    }
}
