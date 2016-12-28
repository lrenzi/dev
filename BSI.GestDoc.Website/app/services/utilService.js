app.service("utilService", ["$rootScope", function ($rootScope) {

    this.mensagemSucesso = function (mensagem) {

        var listaMensagem = [];
        if (Array.isArray(mensagem)) {
            for (var i = 0; i < mensagem.length; i++) {
                listaMensagem.push("▪ " + mensagem[i]);
            }
        } else {
            listaMensagem.push("▪ " + mensagem);
        }

        var objMensagem = {};
        objMensagem.tipoMensagem = 'alert alert-success';
        $rootScope.exibirMensagem = true;
        objMensagem.titulo = 'Sucesso';
        objMensagem.mensagem = listaMensagem;
        $rootScope.mensagens.push(objMensagem)
    }

    this.mensagemInformativo = function ( mensagem) {

        var listaMensagem = [];
        if (Array.isArray(mensagem)) {
            for (var i = 0; i < mensagem.length; i++) {
                listaMensagem.push("▪ " + mensagem[i]);
            }
        } else {
            listaMensagem.push("▪ " + mensagem);
        }

        var objMensagem = {};
        objMensagem.tipoMensagem = 'alert alert-info';
        $rootScope.exibirMensagem = true;
        objMensagem.titulo = 'Informativo';
        objMensagem.mensagem = listaMensagem;
        $rootScope.mensagens.push(objMensagem);
    }

    this.mensagemAlerta = function (mensagem) {

        var listaMensagem = [];
        if (Array.isArray(mensagem)) {
            for (var i = 0; i < mensagem.length; i++) {
                listaMensagem.push("▪ " + mensagem[i]);
            }
        } else {
            listaMensagem.push("▪ " + mensagem);
        }

        var objMensagem = {};
        objMensagem.tipoMensagem = 'alert alert-warning';
        $rootScope.exibirMensagem = true;
        objMensagem.titulo = 'Alerta';
        objMensagem.mensagem = listaMensagem;
        $rootScope.mensagens.push(objMensagem);
    }

    this.mensagemErro = function (mensagem) {

        var listaMensagem = [];
        if (Array.isArray(mensagem)) {
            for (var i = 0; i < mensagem.length; i++) {
                listaMensagem.push("▪ " + mensagem[i]);
            }
        } else {
            listaMensagem.push("▪ " + mensagem);
        }

        var objMensagem = {};
        objMensagem.tipoMensagem = 'alert alert-danger';
        $rootScope.exibirMensagem = true;
        objMensagem.titulo = 'Erro';
        objMensagem.mensagem = listaMensagem;
        $rootScope.mensagens.push(objMensagem);
    }

    this.limparMensagem = function () {
        $rootScope.mensagens = [];
        $rootScope.exibirMensagem = false;
    }
    this.defirniTitulos = function (titulo, subTitulo) {
        $rootScope.titulo = titulo;
        $rootScope.subTitulo = subTitulo;
    }
}]);