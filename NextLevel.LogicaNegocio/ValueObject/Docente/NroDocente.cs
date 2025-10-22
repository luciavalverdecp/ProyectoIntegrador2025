using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ValueObject.Docente
{
    [ComplexType]
    public record NroDocente
    {
        public int NroDeDocente { get; init; }

        public NroDocente() { }

        public NroDocente(int NroDoc)
        {
            this.NroDeDocente = NroDoc;
        }

        public override string ToString()
        {
            return NroDeDocente.ToString();
        }
    }
}
