(function ()
{
    'use strict';

    angular
        .module('app.usuario', [])
        .config(config);

    /** @ngInject */
    function config($stateProvider, msNavigationServiceProvider)
    {
        // State
        $stateProvider
            .state('app.usuario', {
                url    : '/usuario',
                views  : {
                    'content@app': {
                        templateUrl: 'app/main/usuario/lista/lista.view.html',
                        controller : 'ListaUsuarioController as vm'
                    }
                }
            })
            .state('app.novoUsuario', {
                url    : '/usuario/novo',
                views  : {
                    'content@app': {
                        templateUrl: 'app/main/usuario/formulario/novo.view.html',
                        controller : 'UsuarioController as vm'
                    }
                }
            });
        

        // Navigation
        msNavigationServiceProvider.saveItem('acesso', {
            title : 'Acesso',
            group : true,
            weight: 1
        });

        msNavigationServiceProvider.saveItem('acesso.usuario', {
            title    : 'Usuario',
            icon     : 'icon-person-box',
            state    : 'app.usuario',
            /*stateParams: {
                'param1': 'page'
             },*/            
            weight   : 1
        });
    }
})();