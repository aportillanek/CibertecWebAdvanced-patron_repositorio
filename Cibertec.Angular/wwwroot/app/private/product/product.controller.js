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