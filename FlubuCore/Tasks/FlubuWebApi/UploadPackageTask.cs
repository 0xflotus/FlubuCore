﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FlubuCore.Context;
using FlubuCore.WebApi.Client;
using FlubuCore.WebApi.Model;

namespace FlubuCore.Tasks.FlubuWebApi
{
    public class UploadPackageTask : WebApiBaseTask<UploadPackageTask, int>
    {
	    private string _packageSearchPattern;

	    private string _directoryPath;

		public UploadPackageTask(IWebApiClient client, string directoryPath) : base(client)
		{
		    _directoryPath = directoryPath;
	    }

		/// <summary>
		/// The search string to match against the names of files(packages). This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions. The default pattern is "*", which returns all files.
		/// </summary>
		/// <param name="packageSearchPattern"></param>
		/// <returns></returns>
		public UploadPackageTask PackageSearchPattern(string packageSearchPattern)
	    {
		    _packageSearchPattern = packageSearchPattern;
		    return this;
	    }

		protected override int DoExecute(ITaskContextInternal context)
	    {
			
		    Task<int> task = DoExecuteAsync(context);

		    return task.GetAwaiter().GetResult();
	    }

	    protected override async Task<int> DoExecuteAsync(ITaskContextInternal context)
	    {
			PrepareWebApiClient(context);
			await WebApiClient.UploadPackageAsync(new UploadPackageRequest
		    {
			    PackageSearchPattern = _packageSearchPattern,
				DirectoryPath = _directoryPath
		    });

		    return 0;
	    }
	}
}
