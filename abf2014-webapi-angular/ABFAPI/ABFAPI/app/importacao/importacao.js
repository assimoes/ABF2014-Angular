(function () {
    'use strict';

    var controllerId = 'importacao';

    angular.module('app').controller(controllerId,
        ['$scope', 'ambifacservice', 'common', importacao]);

    function importacao($scope, ambifacservice, common) {

        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;


        vm.activate = activate;
        vm.title = 'Ambifac 2014';
        vm.busyMessage = 'Aguarde ...';
        vm.billingResponse = [];
        vm.ProcessBilling = ProcessBilling;
        vm.Phase = '';
        vm.PhaseDesc = '';
        vm.IsProcessing = false;

        function activate() {
            common.activateController([], controllerId)
               .then(function () { log('Importacao de Clientes Activada'); });
        }

        function ProcessBilling() {
            vm.IsProcessing = true;
            vm.billingResponse = [];
            vm.PhaseDesc = 'A obter credencial de facturação';
            ambifacservice.GetAccessToken().then(function (t) {

                var response = t[0].data;
                var token = response.replace(/"/g, '');
                vm.PhaseDesc = 'A definir cliente(s) a faturar...Aguarde';
                ambifacservice.ExecutePhase0(token).then(function (data) {
                    vm.Phase = data[0].data[0].Phase;
                    vm.PhaseDesc = 'A preencher tabelas auxiliares...Aguarde';
                    ambifacservice.ExecutePhase1(token).then(function (data) {
                        vm.Phase = data[0].data[0].Phase;
                        vm.PhaseDesc = 'A aplicar regras de Faturação...Aguarde';
                        ambifacservice.ExecutePhase2(token).then(function (data) {
                            vm.Phase = data[0].data[0].Phase;
                            vm.billingResponse = data[0].data;
                            vm.PhaseDesc = 'A validar qualidade dos dados faturados...Aguarde';
                            ambifacservice.ExecutePhase3(token).then(function (data) {
                                vm.Phase = data[0].data[0].Phase;
                            }).then(function (data) {
                                vm.PhaseDesc = 'Processo concluido.';
                                vm.Phase = '8';//fim
                                vm.IsProcessing = false;
                            })
                        })
                    })
                })
            });
        }

        activate();

    }
})();