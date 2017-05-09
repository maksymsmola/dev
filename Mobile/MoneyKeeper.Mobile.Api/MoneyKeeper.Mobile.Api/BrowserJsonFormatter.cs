﻿using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace MoneyKeeper.Mobile.Api
{
    public class BrowserJsonFormatter : JsonMediaTypeFormatter
    {
        public BrowserJsonFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            this.SerializerSettings.Formatting = Formatting.None;
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
            headers.ContentEncoding.Add("utf-8");
        }
    }
}