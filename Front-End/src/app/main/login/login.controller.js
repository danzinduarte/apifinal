(function ()
{
    'use strict';

    angular
        .module('app.login')
        .controller('LoginController', LoginController);

    /** @ngInject */
    function LoginController($localStorage,$state)
    {
        var vm = this;
        
        function init(){

        }
        init();

        vm.login = login;

        function login(){
            $state.go('app.cliente')
        }
    }
})();