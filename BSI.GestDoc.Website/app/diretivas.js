app.directive('titulos', function () {
    return {
        template: '<h2>{{titulo}}</h2> <h4>{{subTitulo}}</h4>'
    };
});

app.directive('alphaNumericos', function () {
    return {
        retrict: 'A',
        link: function ($scope, elem, attr, model) {
            

            elem.on('keypress', function (e) {
                var charCode = (e.which) ? e.which : e.keyCode;
                var str = String.fromCharCode(charCode);
                var expressao = /[a-zA-Z0-9]/g;
                var reg = new RegExp(expressao);
                if (!reg.test(str)) {
                    e.preventDefault();
                    return false;
                }
                return true;
            }).on('focusout', function (e) {
                this.value = this.value.replace(/[^a-zA-Z0-9]/g, '')
            }).on('paste', function (e) {
                var campo = this;
                setTimeout(function () {
                    campo.value = campo.value.replace(/[^a-zA-Z0-9]/g, '');
                });
            });
        }
    }
});

app.directive('alphaNumericosEspecial', function () {
    return {
        retrict: 'A',
        link: function ($scope, elem, attr, model) {
            
            elem.on('keypress', function (e) {
                var charCode = (e.which) ? e.which : e.keyCode;
                var str = String.fromCharCode(charCode);
                var expressao = /[^#&]/;
                var reg = new RegExp(expressao);
                if (!reg.test(str)) {
                    e.preventDefault();
                    return false;
                }
                return true;
            }).on('focusout', function (e) {
                this.value = this.value.replace(/[#&]/, '')
            }).on('paste', function (e) {
                var campo = this;
                setTimeout(function () {
                    campo.value = campo.value.replace(/[#&]/, '');
                });
            });
        }
    }
});

app.directive("focusOn", function ($timeout) {
    return {
        restrict: "A",
        link: function (scope, element, attrs) {
            scope.$on(attrs.focusOn, function (e) {
                $timeout((function () {
                    element[0].focus();
                }), 10);
            });
        }
    };
});

app.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });

                event.preventDefault();
            }
        });
    };
});