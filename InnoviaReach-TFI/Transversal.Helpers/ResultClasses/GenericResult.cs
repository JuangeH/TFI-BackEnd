using System;
using System.Collections.Generic;
using System.Text;

namespace Transversal.Helpers.ResultClasses
{
    public class GenericResult : IGenericResult
    {
        public string Message { get; set; }

        public IList<string> Errors { get; set; }

        public virtual bool Success { get => Errors.Count == 0; }

        public IList<string> Issues { get; set; }

        public virtual void AddError(string error) => Errors.Add(error);

        public virtual void AddIssue(string issue) => Issues.Add(issue);

        public GenericResult()
        {
            Errors = new List<string>();
            Issues = new List<string>();
        }

        public GenericResult(params string[] errors) : this() => Errors = errors;
    }

    public partial class GenericResult<T> : GenericResult, IGenericResult<T> where T : class
    {
        public T Data { get; set; }

        public bool HasValue { get => !(Data is null); }

        public GenericResult() : base()
        {
        }

        public GenericResult(params string[] errors) : base(errors)
        {
        }

        public GenericResult(T data, params string[] errors) : base(errors)
        {
            Data = data;
        }
    }
}
