using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CadastroMongoDB.Models
{
    public class Empresa
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("Cnpj")]
        public string Cnpj { get; set; }

        [BsonElement("Porte")]
        public string? Porte { get; set; }

        [BsonElement("Situacao")]
        public string Situacao { get; set; }

        [BsonElement("Nome")]
        public string Nome { get; set; }

        [BsonElement("Fantasia")]
        public string? Fantasia { get; set; }

        [BsonElement("Telefone")]
        public string Telefone { get; set; }

        [BsonElement("Email")]
        public string? Email { get; set; }

        [BsonElement("Cep")]
        public string Cep { get; set; }

        [BsonElement("Logradouro")]
        public string Logradouro { get; set; }

        [BsonElement("Complemento")]
        public string? Complemento { get; set; }

        [BsonElement("Numero")]
        public string Numero { get; set; }

        [BsonElement("Bairro")]
        public string Bairro { get; set; }

        [BsonElement("Municipio")]
        public string Municipio { get; set; }

        [BsonElement("Uf")]
        public string Uf { get; set; }

        [BsonElement("Cnae")]
        public string? Cnae { get; set; }

        [BsonElement("Atividade")]
        public string? Atividade { get; set; }
    }
}