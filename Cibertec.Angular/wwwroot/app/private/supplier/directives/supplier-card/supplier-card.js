(function () {
    'use strict';

    angular.module('app')
        .directive('supplierCard', productCard);

    function productCard() {
        return {
            restrict: 'E',
            transclude: true,
            scope:
            {
                id: '@',
                companyName: '@',
                contactName: '@',
                contactTitle: '@',
                city: '@',
                country: '@',
                phone: '@',
                fax: '@'
            },

            templateUrl: 'app/private/supplier/directives/supplier-card/supplier-card.html',
            controller: directiveController



        };


    }

    function directiveController() {



    }

})();