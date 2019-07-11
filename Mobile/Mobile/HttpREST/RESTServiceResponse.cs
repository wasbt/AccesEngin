namespace Mobile.HttpREST
{
    public class RESTServiceResponse<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public T data { get; set; }

		public RESTServiceResponse()
		{
			
		}


		public RESTServiceResponse(T data)
		{
			this.data = data;
		}

		public RESTServiceResponse(bool success, T data)
        {
            this.success = success;
            this.data = data;
        }

        public RESTServiceResponse(bool success, string message)
        {
            this.success = success;
            this.message = message;
        }

        public RESTServiceResponse(bool success, string message,T data)
		{
			this.success = success;
			this.message = message;
			this.data = data;
		}


	}
}
