(function ()
{
    'use strict';

    angular
        .module('app.cliente', [])
        .config(config);

    /** @ngInject */
    function config($stateProvider, msNavigationServiceProvider)
    {
        // State
        $stateProvider
            .state('app.cliente', {
                url    : '/cliente',
                views  : {
                    'content@app': {
                        templateUrl: 'app/main/cliente/lista/lista.view.html',
                        controller : 'ListaClienteController as vm'
                    }
                }
            })
            .state('app.novoCliente', {
                url    : '/cliente/novo',
                views  : {
                    'content@app': {
                        templateUrl: 'app/main/cliente/formulario/novo.view.html',
                        controller : 'ClienteController as vm'
                    }
                }
            });
        

        // Navigation
        msNavigationServiceProvider.saveItem('acesso', {
            title : 'Acesso',
            group : true,
            weight: 1
        });

        msNavigationServiceProvider.saveItem('acesso.cliente', {
            title    : 'Cliente',
            icon     : 'icon-person-box',
            state    : 'app.cliente',
            /*stateParams: {
                'param1': 'page'
             },*/            
            weight   : 1
        });
    }
})();