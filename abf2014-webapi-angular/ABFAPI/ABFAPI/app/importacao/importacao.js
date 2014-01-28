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

        var token = '';
        function activate() {
            common.activateController([], controllerId)
               .then(function () { log('Importacao de Clientes Activada'); });
        }

        function ProcessBilling() {
            vm.IsProcessing = true;
            vm.billingResponse = [];
            vm.PhaseDesc = 'A obter credencial de facturação';
      
            ambifacservice.GetAccessToken().then(_returnedToken, _error)
          
            function _returnedToken(data) {
                var response = data[0].data;
                token = response.replace(/"/g, '');
                vm.PhaseDesc = 'A definir cliente(s) a faturar...Aguarde';
                ambifacservice.ExecutePhase0(token)
                    .then(_executePhase0, _error);
            }

            function _executePhase0(response) {
                vm.Phase = response[0].data[0].Phase;
                vm.PhaseDesc = 'A preencher tabelas auxiliares...Aguarde';
                ambifacservice.ExecutePhase1(token)
                   .then(_executePhase1, _error);
            }

            function _executePhase1(response) {
                vm.Phase = response[0].data[0].Phase;
                vm.PhaseDesc = 'A aplicar regras de Faturação...Aguarde';
                ambifacservice.ExecutePhase2(token)
                   .then(_executePhase2, _error);
            }


            function _executePhase2(response) {
                vm.Phase = response[0].data[0].Phase;
                vm.billingResponse = response[0].data;
                vm.PhaseDesc = 'A validar qualidade dos dados faturados...Aguarde';
                ambifacservice.ExecutePhase3(token)
                   .then(_executePhase3, _error);
            }

            function _executePhase3(response) {
                vm.Phase = response[0].data[0].Phase;
                ambifacservice.DataCleanUp(token)
                    .then(_finishProcessing, _error);
            }

            function _finishProcessing(response){

                token = '';
                vm.PhaseDesc = 'Processo concluido.';
                vm.Phase = '8';
                vm.IsProcessing = false;
            }

            function _error(err) {
                console.log(err.message);
            }
        }

        activate();

    }
})();