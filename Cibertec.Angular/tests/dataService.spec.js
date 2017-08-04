describe('dataService',function(){

    beforeEach(module('app'));
    it('Get Data Ok',inject(function(dataService,$httpBackend){
        $httpBackend.expectGET('/test').respond(200,'Ok');
        dataService.getData('/test').then(function(response){
           expect(response.data).toEqual('Ok');
        });
        $httpBackend.flush();
    })
    );
     it('Post Data Ok',inject(function(dataService,$httpBackend){
        $httpBackend.expectPOST('/test').respond(200,'Ok');
        dataService.postData('/test',{}).then(function(response){
           expect(response.data).toEqual('Ok');
        });
         $httpBackend.flush();
    })
    );
});