(function () {
    angular
        .module('app')
        .factory('authenticationService', authenticationService)
    authenticationService.$inject = ['$http', '$state', 'localStorageService', 'configService', '$q'];

    function authenticationService($http, $state, localStorageService, configService, $q) {
        var service = {};
        service.login = login;
        service.logout = logout;
        return service;

        function login(user) {
            var defer = $q.defer();
            var url = configService.getApiUrl() + '/Token';
            var data = "username=" + user.userName + "&password=" + user.password;
            $http.post(url, data, {
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'

                }


            })
                .then(function (result) {
                    $http.defaults.headers.common.Authorization = 'Bearer ' +
                        result.data.access_token;
                    localStorageService.set('userToken',
                        {
                            token: result.data.access_token,
                            userName: user.userName
                        });
                    configService.setLogin(true);
                    defer.resolve(true);

                },
                function error() {
                    defer.reject(false);

                });
            return defer.promise;

        }

        function logout() {
            $http.defaults.headers.common.Authorization = '';
            localStorageService.remove('userToken');
            configService.setLogin(false);
        }



    }
})();
(function () {
    angular.module('app')
        .factory('dataService', dataService);

    dataService.$inject = ['$http'];

    function dataService($http) {
        var service = {};
        service.getData = getData;
        service.postData = postData;
        service.putData = putData;
        service.deleteData = deleteData;
        return service;
        function getData(url) {
            return $http.get(url);

        }
        function postData(url, data) {
            return $http.post(url, data);
        }
        function putData(url, data) {

            return $http.put(url, data);

        }
        function deleteData(url) {
            return $http.delete(url);


        }



    }
})();
(function ()
 {
    'use strict';
    angular
        .module('app')
        .factory('configService', configService);

    function configService() {
        var service = {};
        var apiUrl = undefined;
        var isLogged = false;
        service.setLogin = setLogin;
        service.getLogin = getLogin;
        service.setApiUrl = setApiUrl;
        service.getApiUrl = getApiUrl;
        return service;

        function setLogin(state) {
            isLogged = state;
        }
        function getLogin() {
            return isLogged;

        }
        function getApiUrl() {
            return apiUrl;

        }
        function setApiUrl(url) {

            apiUrl = url;
        }


    }


})();
(function () {
    angular.module('app')
        .directive('modalPanel', modalPanel);
    function modalPanel()
    {
        return {

            templateUrl: 'app/components/modal/modal-directive.html',
            restrict: 'E',
            transclude: true,
            scope:
            {
                title: '@',
                buttonTitle: '@',
                saveFunction: '=',
                closeFunction: '=',
                readOnly: '=',
                isDelete: '='


            }


        };


    }


})();
(function () {
    'use strict';
    angular.module('app')
        .controller('loginController', loginController);
    loginController.$inject = ['$http', 'authenticationService', 'configService', '$state'];

    function loginController($http, authenticationService, configService, $state) {

        var vm = this;
        vm.user = {};
        vm.title = 'login';
        vm.login = login;
        init();
        function init() {
            if (configService.getLogin())
                $state.go("product");
            authenticationService.logout();

        }
        function login() {

            authenticationService.login(vm.user).then(function (result) {
                vm.showError = false;
                $state.go("home");

            }, function (error) {

                vm.showError = true;
            });

        }
    }







})();
(function () {
    'use strict';
    angular.module('app')
        .controller('customerController', customerController);

    customerController.$inject = ['dataService', 'configService', '$state'];

    function customerController(dataService, configService, $state) {
        var apiUrl = configService.getApiUrl();
        var vm = this;
        vm.customer = {};
        vm.customerList = [];
        init();
        function init() {
            if (!configService.getLogin()) return $state.go('login');
            list();
        }
        function list() {
            dataService.getData(apiUrl + '/customer')
                .then(function (result) {
                    vm.customerList = result.data;
                },
                function (error) {
                    vm.customerList = [];
                    console.log(error);
                });

        }


    }

})();
(function () {
    'use strict';

    angular.module('app')
        .directive('customerCard', customerCard);

    function customerCard() {
        return {
            restrict: 'E',
            transclude: true,
            scope:
            {
                id: '@',
                firstName: '@',
                lastName: '@',
                city: '@',
                country: '@',
                phone: '@'

            },

            templateUrl: 'app/private/customer/directives/customer-card/customer-card.html',
            controller: directiveController



        };


    }

    function directiveController() {



    }

})();
(function () {
    'use strict';
    angular.module('app')
        .controller('productController', productController);

    productController.$inject = ['dataService', 'configService', '$state'];

    function productController(dataService, configService, $state) {
        var apiUrl = configService.getApiUrl();
        var vm = this;
        //Propiedades
        vm.product = {};
        vm.productList = [];
        vm.modalButtonTitle = '';
        vm.readOnly = false;
        vm.isDelete = false;
        vm.modalTitle = '';
        vm.showCreate = false;
        //funciones
        vm.getProduct = getProduct;
        vm.create = create;
        vm.edit = edit;
        vm.delete = productDelete;
        //vm.closeModal=closeModal;
        init();
        function init() {
            if (!configService.getLogin()) return $state.go('login');
            list();
        }
        function list() {
            dataService.getData(apiUrl + '/product')
                .then(function (result) {
                    vm.productList = result.data;
                },
                function (error) {
                    vm.productList = [];
                    console.log(error);
                });

        }



        function getProduct(id) {
            vm.product = null;
            dataService.getData(apiUrl + '/product/' + id)
                .then(function (result) {
                    vm.product = result.data;
                },
                function (error) {
                    vm.product = null;
                    console.log(error);
                });


        }

        function updateProduct(id) {
            if (!vm.product) return;
            dataService.putData(apiUrl + '/product', vm.product)
                .then(function (result) {
                    vm.product = {};
                    list();
                    closeModal();
                },
                function (error) {
                    vm.product = {};
                    console.log(error);
                });


        }

        function createProduct(id) {
            if (!vm.product) return;
            dataService.postData(apiUrl + '/product', vm.product)
                .then(function (result) {
                    getProduct(result.data.id)
                    list();
                    vm.showCreate = true;
                },
                function (error) {

                    console.log(error);
                });


        }
        function deleteProduct()
        {

            dataService.deleteData(apiUrl + '/product/' + vm.product.id)
                .then(function (result) {
                   
                    list();
                    closeModal();
                },
                function (error) {

                    console.log(error);
                });


        }

        function create()
        {
            vm.product = {};
            vm.modalTitle = 'New Product';
            vm.modalButtonTitle = '';
            vm.readOnly = false;
            vm.modalFunction = createProduct;
            vm.isDelete = false;

        }

        function edit()
        {
            vm.showCreate = false;
            vm.modalTitle = 'Edit Product';
            vm.modalButtonTitle = 'update';
            vm.readOnly = false;
            vm.modalFunction = updateProduct;
            vm.isDelete = false;

        }
        function detail()
        {
            vm.modalTitle = 'Created Product';
            vm.modalButtonTitle = '';
            vm.readOnly = true;
            vm.modalFunction = null;
            vm.isDelete = true;

        }

        function productDelete() {
            vm.showCreate = false;

            vm.modalTitle = 'Delete Product';
            vm.modalButtonTitle = 'Delete';
            vm.readOnly = false;
            vm.modalFunction = deleteProduct;
            vm.isDelete = true;
        }
        function closeModal()
        {
            angular.element('#modal-container').modal('hide');

        }


    }

})();
(function () {
    'use strict';

    angular.module('app')
        .directive('productCard', productCard);

    function productCard()
    {
        return {
            restrict: 'E',
            transclude: true,
            scope:
            {
                id: '@',
                productName: '@',
                supplierId: '@',
                unitPrice: '@',
                package: '@',
                isDiscontinued: '='

            },

            templateUrl: 'app/private/product/directives/product-card/product-card.html',
            controller: directiveController



        };


    }

    function directiveController()
    {



    }

})();
(function () {
    'use strict';
    angular.module('app')
        .directive('productForm', productForm);
    function productForm()
    {
        return {
            restrict: 'E',
            scope: {

                product: '='
            },
            templateUrl: 'app/private/product/directives/product-form/product-form.html'



        };


    }

})();
(function () {
    'use strict';
    angular.module('app')
        .controller('supplierController', supplierController);

    supplierController.$inject = ['dataService', 'configService', '$state'];

    function supplierController(dataService, configService, $state) {
        var apiUrl = configService.getApiUrl();
        var vm = this;
        vm.supplier = {};
        vm.supplierList = [];
        init();
        function init() {
            if (!configService.getLogin()) return $state.go('login');
            list();
        }
        function list() {
            dataService.getData(apiUrl + '/supplier')
                .then(function (result) {
                    vm.supplierList = result.data;
                },
                function (error) {
                    vm.supplierList = [];
                    console.log(error);
                });

        }


    }

})();
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