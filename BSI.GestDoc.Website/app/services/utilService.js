app.service("utilService", ["$rootScope", function ($rootScope) {

    var exibirMensagemReqest = false;


    this.mensagemSucesso = function (mensagem, exibirMensagem) {

        exibirMensagemReqest = exibirMensagem;

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
        $rootScope.mensagens.push(objMensagem);
        $rootScope.textoBotao = "";
        $rootScope.functionName = "";
        $rootScope.exibirBotao = false;
    };

    this.mensagemInformativo = function (mensagem) {

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
        $rootScope.textoBotao = "";
        $rootScope.functionName = "";
        $rootScope.exibirBotao = false;
    };

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
        $rootScope.textoBotao = "";
        $rootScope.functionName = "";
        $rootScope.exibirBotao = false;
    };

    this.mensagemAlertaButtom = function (mensagem, textoBotao, functionName) {

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
        $rootScope.textoBotao = textoBotao;
        $rootScope.functionName = functionName;
        $rootScope.exibirBotao = true;
    };

   


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
        $rootScope.textoBotao = "";
        $rootScope.functionName = "";
        $rootScope.exibirBotao = false;
    };

    this.mensagemModalAlerta = function (mensagem) {

        var listaMensagem = [];
        if (Array.isArray(mensagem)) {
            for (var i = 0; i < mensagem.length; i++) {
                listaMensagem.push("▪ " + mensagem[i]);
            }
        } else {
            listaMensagem.push("▪ " + mensagem);
        }

        var objMensagem = {};
        objMensagem.tipoMensagemModal = 'alert alert-warning';
        $rootScope.exibirModalMensagem = true;
        objMensagem.titulo = 'Alerta';
        objMensagem.mensagemModal = listaMensagem;
        $rootScope.mensagens.push(objMensagem);
    };

    this.mensagemModalErro = function (mensagem) {

        var listaMensagem = [];
        if (Array.isArray(mensagem)) {
            for (var i = 0; i < mensagem.length; i++) {
                listaMensagem.push("▪ " + mensagem[i]);
            }
        } else {
            listaMensagem.push("▪ " + mensagem);
        }

        var objMensagem = {};
        objMensagem.tipoMensagemModal = 'alert alert-danger';
        $rootScope.exibirModalMensagem = true;
        objMensagem.titulo = 'Erro';
        objMensagem.mensagemModal = listaMensagem;
        $rootScope.mensagens.push(objMensagem);
    };

    this.limparMensagem = function () {

        $rootScope.mensagens = [];
        $rootScope.exibirMensagem = false;

    };

    this.limparMensagemModal = function () {

        $rootScope.mensagemModal = [];
        $rootScope.exibirModalMensagem = false;

    };

    this.limparMensagemRequest = function () {
        if (!exibirMensagemReqest) {
            $rootScope.mensagens = [];
            $rootScope.exibirMensagem = false;
        }
    };


    this.definirTitulos = function (titulo, subTitulo) {
        $rootScope.titulo = titulo;
        $rootScope.subTitulo = subTitulo;
    };
}]);