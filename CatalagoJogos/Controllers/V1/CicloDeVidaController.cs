using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalagoJogos.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CicloDeVidaController : ControllerBase
    {
        private readonly IExamploSingletom _examploSingletom1;
        private readonly IExamploSingletom _examploSingletom2;

        private readonly IExamploScoped _examploScoped1;
        private readonly IExamploScoped _examploScoped2;

        private readonly IExamploTransient _examploTransient1;
        private readonly IExamploTransient _examploTransient2;

        public CicloDeVidaController(IExamploSingletom examploSingletom1, IExamploSingletom examploSingletom2 , IExamploScoped examploScoped1 , IExamploScoped examploScoped2 , IExamploTransient examploTransient1 , IExamploTransient examploTransient2)
        {
            _examploSingletom1 = examploSingletom1;
            _examploSingletom2 = examploSingletom2;
            _examploScoped1 = examploScoped1;
            _examploScoped2 = examploScoped2;
            _examploTransient1 = examploTransient1;
            _examploTransient2 = examploTransient2;

        }

        [HttpGet]

        public Task<string> Get()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Singletom 1: {_examploSingletom1.Id}");
            stringBuilder.AppendLine($"Singletom 2: {_examploSingletom2.Id}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Scoped 1: {_examploScoped1.Id}");
            stringBuilder.AppendLine($"Scoped 2: {_examploScoped2.Id}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Transient 1: {_examploTransient1.Id}");
            stringBuilder.AppendLine($"Transient 2: {_examploTransient2.Id}");

            return Task.FromResult(stringBuilder.ToString());
        }

        public interface IExamploGeral
        {
            public Guid Id { get; }
        }

        public interface IExamploSingletom : IExamploGeral
        { }

        public interface IExamploScoped : IExamploGeral
        { }

        public interface IExamploTransient : IExamploGeral
        { }

        public class ExemploCicloDeVida : IExamploSingletom , IExamploScoped , IExamploTransient
        {

            private readonly Guid _guid;

            public ExemploCicloDeVida()
            {
                _guid = Guid.NewGuid();
            }
            public Guid Id => _guid;
        }
    }
}
