(function () {
    'use strict';

    // Factory name is handy for logging
    var serviceId = 'ambifacservice';

    // Define the factory on the module.
    // Inject the dependencies. 
    // Point to the factory definition function.
    // TODO: replace app with your module name
    angular.module('app').factory(serviceId, ['$http','$q', ambifacservice]);

    function ambifacservice($http,$q) {
        // Define the functions and properties to reveal.
        var service = {
            performBilling: performBilling,
            ExecutePhase0: ExecutePhase0,
            ExecutePhase1: ExecutePhase1,
            ExecutePhase2: ExecutePhase2,
            ExecutePhase3: ExecutePhase3,
            GetAccessToken: GetAccessToken,
            DataCleanUp: DataCleanUp
         
        };

        return service;

        function performBilling() {

            $http.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
            $http.defaults.headers.common['Access-Control-Allow-Headers'] = 'GET, POST, PUT, DELETE';
            return $q.all([$http.get('http://localhost:18855/api/Billing/performbilling/9216bb7a-c211-403b-a2c2-b7689833ffd6/112/1003772+2090461')]);
        }

        function GetAccessToken(token) {
            return $q.all([$http.get('http://localhost:18855/api/Billing/GetBillingtoken')]);
        }

        function ExecutePhase0(token) {
            return $q.all([$http.get('http://localhost:18855/api/Billing/ExecutePhase0/' + token + '/112/1003772,2090461')]);
        }

        function ExecutePhase1(token) {
            return $q.all([$http.get('http://localhost:18855/api/Billing/ExecutePhase1/' + token +'/112')]);
        }

        function ExecutePhase2(token) {
            return $q.all([$http.get('http://localhost:18855/api/Billing/ExecutePhase2/' + token + '/112')]);
        }

        function ExecutePhase3(token) {
            return $q.all([$http.get('http://localhost:18855/api/Billing/ExecutePhase3/' + token + '/112')]);
        }

        function DataCleanUp(token) {
            return $q.all([$http.get('http://localhost:18855/api/Billing/DataCleanUp/' + token)]);
        }
    }
})();