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