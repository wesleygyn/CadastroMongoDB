$(document).ready(function(){
    console.log("MAIN JS carregado...");

    // captura o valor ao digitar no input
    $("#cnpj").on("keyup", function(){
        // remove os caracteres (mascara)
        var cnpj = $(this).val().replace(/[^\d]+/g,'');

        // verifica o tamanho do valor informado
        if(cnpj.length == 14){
            getReceita(cnpj, function(data){
                if(data.status == "ERROR"){
                    $("#feedback_cnpj").html(data.message);
                    $("#cnpj").addClass("is-invalid");
                }else{
                    $("#feedback_cnpj").html("");
                    $("#cnpj").removeClass("is-invalid").addClass("is-valid");
                    
                    /**
                     * preenche o formulário com os dados de retorno
                     *  */ 

                    // dados
                    $("#cnpj").val(data.cnpj);
                    $("#porte").val(data.porte);
                    $("#situacao").val(data.situacao);

                    // identificação
                    $("#nome").val(data.nome);
                    $("#fantasia").val(data.fantasia);
                    
                    // contato
                    $("#telefone").val(data.telefone);
                    $("#email").val(data.email);

                    // endereco
                    $("#logradouro").val(data.logradouro);
                    $("#complemento").val(data.complemento);
                    $("#bairro").val(data.bairro);
                    $("#numero").val(data.numero);
                    $("#cep").val(data.cep);
                    $("#municipio").val(data.municipio);
                    $("#uf").val(data.uf);

                    // atividade principal
                    $("#cnae").val(data.atividade_principal[0].code);
                    $("#atividade").val(data.atividade_principal[0].text);

                    // remove o conteudo para preencher com novo
                    $("#divAtividades").html("");

                    // atividade secundarias
                    if(data.atividades_secundarias.length > 0){
                        $.each(data.atividades_secundarias, function(i, s){
                            var divAtividades = $("#divAtividades");                            

                            var a = '<div class="col-md-3">';
                            a += '<label for="cnae-sec['+ i +']">CNAE</label>';
                            a += '<input type="text" class="form-control" id="cnae-sec['+ i +']" name="cnae-sec['+ i +']" value="'+ s.code +'">'
                            a += '</div>';
                            a += '<div class="col-md-9">';
                            a += '<label for="atividades-secundarias['+ i +']">Atividade</label>';
                            a += '<input type="text" class="form-control" id="atividades-secundarias['+ i +']" name="atividades-secundarias['+ i +']" value="'+ s.text +'">';
                            a += '</div>';

                            divAtividades.append(a);
                        }); 
                    }

                    console.log(data);
                }
            });            
        }else{
            console.log("CNPJ incorreto");
        }
    });
});

// função para consumir a API receitaWS
function getReceita(cnpj, callback){
    $.ajax({
        url: "https://www.receitaws.com.br/v1/cnpj/"+ cnpj,
        method:'GET',
        dataType: 'jsonp',
    }).done(function(data) {
        callback(data);
    });
}
