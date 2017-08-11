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
        vm.showError =false;
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
    'use strict'
    angular.module('app')
        .controller('csvController', loginController);

    function loginController(configService, $state) {
        var vm = this;
        vm.csvLines = [];
        vm.processFile = processFile;
        var fileInput = document.getElementById("csvViewer");
        init();
        function init() {

            if (!configService.getLogin()) $state.go("login");


            //  fileInput.addEventListener('change', readFile);
           
        }

        function processFile()
        {
            vm.csvLines = [];

            readFile(function (result) {
                var list = [];
                var totalLines = result.length;
                var count = 0;
                var csvWorker = new Worker("/js/worker.js");
                csvWorker.addEventListener('message', function (message) {
                    
                    list.push(message.data);
                   
                    console.log("Processing...");
                    count++;
                    if (count >= totalLines) csvWorker.terminate();

                });
                for (var i = 0; i < result.length;i++)
                {
                   
                    csvWorker.postMessage(result[i]);
                }
             
            });


        }

        function readFile(callback)
        {
            var reader = new FileReader();
            //var list = [];
            reader.onload = function ()
            {
               // var lines = reader.result.split("r\n");

                //for (var i = 0; i < lines.length;i++)
                //{
                //    list.push(formatLine(lines[i]))

                //}
                //vm.csvLines.push(list);
                //console.log(list);
               
                return callback(reader.result.split("\r\n"));
            };
            reader.readAsBinaryString(fileInput.files[0]);

        }
        function formatLine(line)
        {
            var splittextline = line.split(",");

            var member = {
                member_no: splittextline[0],
                lastname: splittextline[1],
                firstname: splittextline[2],
                middleinitial: splittextline[3],
                street: splittextline[4],
                city: splittextline[5],
                state_prov: splittextline[6],
                country: splittextline[7],
                mail_code: splittextline[8],
                phone_no: splittextline[9],
                issue_dt: splittextline[10],
                expr_dt: splittextline[11],
                corp_no: splittextline[12],
                prev_balance: splittextline[13],
                curr_balance: splittextline[14],
                member_code: splittextline[15]

            }
            return member;
          
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
        vm.modalButtonTitle = '';
        vm.readOnly = false;
        vm.isDelete = false;
        vm.modalTitle = '';
        vm.showCreate = false;
     

        vm.totalRecords = 0;
        vm.currentPage = 1;
        vm.maxSize = 10;
        vm.itemPerPage = 25;

        //funciones
        vm.getCustomer = getCustomer;
        vm.create = create;
        vm.edit = edit;
        vm.delete = customerDelete;
        vm.pageChanged = pageChanged;


        init();
        function init() {
            if (!configService.getLogin()) return $state.go('login');
            configurePagination();
        }
        function configurePagination() {

            var widthScreen = (window.innerWidth > 0) ? window.innerWidth : screen.width;
            if (widthScreen < 420) vm.maxSize = 5;
            totalRecords();
        }
        function pageChanged()
        {
            getPageRecords(vm.currentPage);

        }
        function totalRecords()
        {
            dataService.getData(apiUrl + '/customer/count')
                .then(function (result) {
                    vm.totalRecords = result.data;
                    getPageRecords(vm.currentPage);
                }, function (error)
                {
                    vm.customerList = [];
                    console.log(error);
                }
                    );
        }
        function getPageRecords(page)
        {
            dataService.getData(apiUrl + '/customer/list/' + page + '/' + vm.itemPerPage)
                .then(function (result) {
                    vm.customerList = result.data;

                },
                function (error) {
                    vm.customerList = [];
                    console.log(error);
                });
        }

        function getCustomer(id)
        {
            vm.customer = null;
            dataService.getData(apiUrl + '/customer/' + id)
                .then(function (result) {
                    
                    vm.customer = result.data;
                },
                function (error) {
                    vm.customer = null;
                    console.log(error);
                });
        }

        function updateCustomer()
        {
            if (!vm.customer) return;
            dataService.putData(apiUrl + '/customer', vm.customer)
                .then(function (result) {
                    vm.customer = {};
                    pageChanged();
                    closeModal();
                },
                function (error) {
                    vm.product = {};
                    console.log(error);
                });

        }

        function createCustomer() {
            if (!vm.customer) return;
            dataService.postData(apiUrl + '/customer', vm.customer)
                .then(function (result) {
                    getCustomer(result.data.id);
                    pageChanged();
                    vm.showCreate = true;
                    vm.customer = {};
                },
                function (error) {

                    console.log(error);
                });


        }

        function deleteCustomer() {

            dataService.deleteData(apiUrl + '/customer/' + vm.customer.id)
                .then(function (result) {

                    pageChanged();
                    closeModal();
                },
                function (error) {

                    console.log(error);
                });


        }

        function create() {
            vm.customer = {};
            vm.modalTitle = 'New Customer';
            vm.modalButtonTitle = 'Create';
            vm.readOnly = false;
            vm.modalFunction = createCustomer;
            vm.isDelete = false;

        }

        function edit() {
            vm.showCreate = false;
            vm.modalTitle = 'Edit Customer';
            vm.modalButtonTitle = 'update';
            vm.readOnly = false;
            vm.modalFunction = updateCustomer;
            vm.isDelete = false;

        }
        function detail() {
            vm.modalTitle = 'Created Customer';
            vm.modalButtonTitle = '';
            vm.readOnly = true;
            vm.modalFunction = null;
            vm.isDelete = true;

        }

        function customerDelete() {
            vm.showCreate = false;

            vm.modalTitle = 'Delete Customer';
            vm.modalButtonTitle = 'Delete';
            vm.readOnly = false;
            vm.modalFunction = deleteCustomer;
            vm.isDelete = true;
        }
        function closeModal() {
            angular.element('#modal-container').modal('hide');

        } 
        //function list() {
        //    dataService.getData(apiUrl + '/customer')
        //        .then(function (result) {
        //            vm.customerList = result.data;
        //        },
        //        function (error) {
        //            vm.customerList = [];
        //            console.log(error);
        //        });

        //}


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
(function ()
{
    'use-strict';
    angular.module('app')
        .directive('customerForm', customerForm);
    function customerForm()
    {

        return {

            restrict: 'E',
            scope: {

                customer: '='
            },
            templateUrl: 'app/private/customer/directives/customer-form/customer-form.html'


        };


    }




} )();
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



        vm.totalRecords = 0;
        vm.currentPage = 1;
        vm.maxSize = 10;
        vm.itemPerPage = 25;
        vm.notificationHubProxy = {};
        vm.blockedIds = [];
        vm.isEdited = false;
        //funciones
        vm.getProduct = getProduct;
        vm.create = create;
        vm.edit = edit;
        vm.delete = productDelete;
        vm.pageChanged = pageChanged;
        vm.closeModal = closeModal;
        init();
        function init() {
            if (!configService.getLogin()) return $state.go('login');
           
            configurePagination();
            startSignalR();

        }

        function startSignalR()
        {
            $.connection.hub.logging = true;
            vm.notificationHubProxy = $.connection.notificationHub;

            vm.notificationHubProxy.client.addProductId = function (list) {
                console.log(list);
                vm.blockedIds=list;
            };
            vm.notificationHubProxy.client.removeProductId = function (list) {
                console.log(list);
                vm.blockedIds = list;
            };
           
            $.connection.hub.start().done(function () {

                console.log("Hub start success");
            }).fail(function (error) {
                console.log(error);
            });


        }
        function configurePagination()
        {

            var widthScreen = (window.innerWidth > 0) ? window.innerWidth : screen.width;
            if (widthScreen < 420) vm.maxSize = 5;
            totalRecords();
        }
        function pageChanged()
        {
            getPageRecords(vm.currentPage);
        }
        function totalRecords()
        {
            dataService.getData(apiUrl + '/product/count')
                .then(function (result) {
                    vm.totalRecords = result.data;
                    getPageRecords(vm.currentPage);
                },
                function (error) {
                    vm.productList = [];
                    console.log(error);
                });

        }

        function getPageRecords(page)
        {
            dataService.getData(apiUrl + '/product/list/' + page + '/' + vm.itemPerPage)
                .then(function (result) {
                    vm.productList = result.data;
                  
                },
                function (error) {
                    vm.productList = [];
                    console.log(error);
                });

        }

        //function list() {
        //    dataService.getData(apiUrl + '/product')
        //        .then(function (result) {
        //            vm.productList = result.data;
        //        },
        //        function (error) {
        //            vm.productList = [];
        //            console.log(error);
        //        });

        //}

        function checkId(id)
        {
            var index = vm.blockedIds.indexOf(id);
            return (index > -1);

        }

        function getProduct(id) {
            vm.isEdited = false;
            if (checkId(id))
            {
                vm.isEdited = true;
                return;
            }
           
            vm.product = null;
            dataService.getData(apiUrl + '/product/' + id)
                .then(function (result) {
                    vm.product = result.data;
                    vm.notificationHubProxy.server.addProductId(vm.blockedIds,id);
                },
                function (error) {
                    vm.product = null;
                    console.log(error);
                });


        }

        function updateProduct() {
            if (!vm.product) return;
            dataService.putData(apiUrl + '/product', vm.product)
                .then(function (result) {
                    vm.product = {};
                 //   list();
                    pageChanged();
                    closeModal();
                },
                function (error) {
                    vm.product = {};
                    console.log(error);
                });


        }

        function createProduct() {
            if (!vm.product) return;
            dataService.postData(apiUrl + '/product', vm.product)
                .then(function (result) {
                    getProduct(result.data.id);
                    pageChanged();
                   
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
                   
                    pageChanged();
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
            vm.modalButtonTitle = 'Create';
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
            if (vm.isEdited==false)
                angular.element('#modal-container').modal('show');


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
            if (vm.product)
                vm.notificationHubProxy.server.removeProductId(vm.blockedIds,vm.product.id);
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
        .directive('supplierCard', supplierCard);

    function supplierCard() {
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