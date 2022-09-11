using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Persistence.Services
{
	public class PhotoService : IPhotoService
	{

		private readonly Cloudinary? _cloudinary;
		public PhotoService()
		{
			var acc = new Account
			(
				"webapi",
				"592423288134115",
				"IiyJX9O34h4qbQnd_pKvgelulFo"
			);
			_cloudinary = new Cloudinary(acc);
		}



		//public bool ImageSize(IFormFile file, int size)
		//	=> file.Length / 1024 > size;


		//public bool IsImage(IFormFile file)
		//	=> file.ContentType.Contains("image/");


		public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
		{
			var uploadResult = new ImageUploadResult();
			
				using var stream = file.OpenReadStream();
				var uploadParams = new ImageUploadParams
				{
					File = new FileDescription(file.FileName, stream),
					Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
				};

				uploadResult = await _cloudinary.UploadAsync(uploadParams);
			
			return uploadResult;
		}

		public async Task<DeletionResult> DeletePhotoAsync(string publicId)
		{
			var deleteParams = new DeletionParams(publicId);

			var result = await _cloudinary.DestroyAsync(deleteParams);

			return result;
		}

		

		
	}
}
