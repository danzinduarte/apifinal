(function ()
{
    'use strict';

    angular
        .module('app.sample')
        .controller('SampleController', SampleController);

    /** @ngInject */
    function SampleController(SampleData)
    {
        var vm = this;

        // Data
        vm.helloText = SampleData.data.helloText;

        vm.teste = [
            {
                nome: "Ricardo de Oliveira",
                idade: 20,
                cidade: "Nanuque"
            },
            {
                nome: "Italo Leandro",
                idade: 21,
                cidade: "Itanhem"
            },
            {
                nome: "Danilo Duarte",
                idade: 29,
                cidade: "Itamaraju"
            }
        ]
        // Methods

        //////////
    }
})();
