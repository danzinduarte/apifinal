(function ()
{
    'use strict';

    angular
        .module('app.cliente')
        .controller('ListaClienteController', ListaClienteController);

    /** @ngInject */
    function ListaClienteController(clienteService)
    {
        var vm = this;

        vm.gridService = {
            query : {
                order: 'nome',
                limit: 5,
                page: 1
            },
            selected : [],

            loadData : function(){
                return clienteService.getDataMockup().then(function(records){
                    console.log(records)
                    vm.data = records
                })   
            }
        }

        function init(){
            vm.gridService.loadData()
        }
        init()
        
    }
})();


