(function ()
{
    'use strict';

    angular
        .module('app.usuario')
        .controller('ListaUsuarioController', ListaUsuarioController);

    /** @ngInject */
    function ListaUsuarioController(usuarioService)
    {
        var vm = this;

        vm.gridService = {
            query : {
                order: 'email',
                limit: 5,
                page: 1
            },
            selected : [],

            loadData : function(){
                return usuarioService.getDataMockup().then(function(records){
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


