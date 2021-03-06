﻿using System;
using System.Drawing;
using System.Net.Mime;
using System.Web.Mvc;
using Granta.MaterialsWall.DataAccess;
using Granta.MaterialsWall.Images;

namespace Granta.MaterialsWall.Controllers
{
    public sealed class ImageController : Controller
    {
        private readonly ICardRepository cardRepository;
        private readonly IImagePathFormatter imagePathFormatter;
        private readonly IImageToRawDataConverter imageToRawDataConverter;
        private readonly IThumbnailGenerator thumbnailGenerator;
        private readonly IImageDimensionProvider imageDimensionProvider;

        public ImageController(ICardRepository cardRepository, IImagePathFormatter imagePathFormatter, IImageToRawDataConverter imageToRawDataConverter, IThumbnailGenerator thumbnailGenerator, IImageDimensionProvider imageDimensionProvider)
        {
            if (cardRepository == null)
            {
                throw new ArgumentNullException("cardRepository");
            }
            
            if (imagePathFormatter == null)
            {
                throw new ArgumentNullException("imagePathFormatter");
            }

            if (imageToRawDataConverter == null)
            {
                throw new ArgumentNullException("imageToRawDataConverter");
            }

            if (thumbnailGenerator == null)
            {
                throw new ArgumentNullException("thumbnailGenerator");
            }

            if (imageDimensionProvider == null)
            {
                throw new ArgumentNullException("imageDimensionProvider");
            }
            
            this.cardRepository = cardRepository;
            this.imagePathFormatter = imagePathFormatter;
            this.imageToRawDataConverter = imageToRawDataConverter;
            this.thumbnailGenerator = thumbnailGenerator;
            this.imageDimensionProvider = imageDimensionProvider;
        }

        public ActionResult Index(Guid identifier, int index)
        {
            return GetSizedImage(identifier, imageDimensionProvider.GetFullsizeWidth(), index);
        }

        public ActionResult Thumbnail(Guid identifier)
        {
            return GetSizedImage(identifier, imageDimensionProvider.GetThumbnailWidth());
        }

        private ActionResult GetSizedImage(Guid identifier, int maxWidth, int imageIndex = 1)
        {
            var image = GetImage(identifier, imageIndex);
            var originalSize = image.Size;

            if (originalSize.Width > maxWidth)
            {
                double scale = ((double) maxWidth) / originalSize.Width;
                image = thumbnailGenerator.Scale(image, scale);
            }

            return GetImageStream(image);
        }

        private Bitmap GetImage(Guid identifier, int imageIndex)
        {
            var card = cardRepository.GetCard(identifier);
            string imagePath = imagePathFormatter.GetImagePath(card.Id, imageIndex);
            var image = new Bitmap(imagePath);
            return image;
        }

        private FileResult GetImageStream(Bitmap image)
        {
            var bytes = imageToRawDataConverter.GetImageStream(image);

            var contentDisposition = new ContentDisposition
            {
                FileName = "image.jpg", 
                Inline = true
            };

            Response.AppendHeader("Content-Disposition", contentDisposition.ToString());
            return File(bytes, "image/jpeg");
        }
    }
}