//security service
ecollabroApp.service('securityService',['serviceHandler', function (serviceHandler) {

    this.getNavigations = function () {
        return serviceHandler.executeGetService("/SecurityApi/GetNavigations/" + headerController().currentSiteId);
    }
    
    this.getSiteConfiguration=function()
    {
        return serviceHandler.executeGetService("/SecurityApi/GetSiteConfiguration/" + headerController().currentSiteId);
    }
    
    this.getSiteRegistrationAllowed=function() {
        return serviceHandler.executeGetService("/SecurityApi/GetSiteRegistrationAllowed/" + headerController().currentSiteId);
    }

    this.saveSiteFeaturesSettings = function (siteFeaturesSettings) {
        return serviceHandler.executePostService("/SecurityApi/SaveSiteFeaturesSettings/" + headerController().currentSiteId, siteFeaturesSettings);
    }

    this.saveSiteConfiguration = function (siteConfiguration) {
        return serviceHandler.executePostService("/SecurityApi/SaveSiteConfiguration/" + headerController().currentSiteId, siteConfiguration);
    }

    this.getRoles = function () {
        return serviceHandler.executeGetService("/SecurityApi/GetRoles/" + headerController().currentSiteId);
    }

    this.getActiveRoles=function(){
        return serviceHandler.executeGetService("/SecurityApi/GetActiveRoles/" + headerController().currentSiteId);
    }

    this.getRole=function(roleId){
        return serviceHandler.executeGetService("/SecurityApi/GetRole/" + headerController().currentSiteId + "/" + roleId);
    }

    this.deleteRole=function(roleId){
        return serviceHandler.executeGetService("/SecurityApi/DeleteRole/" + headerController().currentSiteId + "/" + roleId);
    }

    this.saveRole=function(role){
        return serviceHandler.executePostService("/SecurityApi/SaveRole/" + headerController().currentSiteId, role);
    }

    this.getRoleFeatures=function(roleId){
        return serviceHandler.executeGetService("/SecurityApi/GetRoleFeatures/" + headerController().currentSiteId + "/" + roleId);
    }

    this.saveRoleFeatures=function(roleFeatures){
        return serviceHandler.executePostService("/SecurityApi/SaveRoleFeatures/" + headerController().currentSiteId, roleFeatures);
    }
    
    this.getSites=function(){
        return serviceHandler.executeGetService("/SecurityApi/GetSites/");
    }

    this.getSite = function (siteId) {
        return serviceHandler.executeGetService("/SecurityApi/GetSite/"+siteId);
    }

    this.deleteSite=function(siteId){
        return serviceHandler.executeGetService("/SecurityApi/DeleteSite/" + siteId);
    }

    this.copySite=function(sourceSiteId){
        return serviceHandler.executeGetService("/SecurityApi/CopySite/" + sourceSiteId);
    }

    this.saveSite=function(site){
        return serviceHandler.executePostService("/SecurityApi/SaveSite" ,site);
    }

    this.getSiteFeatures=function(siteId){
        return serviceHandler.executeGetService("/SecurityApi/GetSiteFeatures/" + siteId);
    }

    this.getSiteFeaturesSettings=function(){
        return serviceHandler.executeGetService("/SecurityApi/GetSiteFeaturesSettings/" + headerController().currentSiteId);
    }

    this.saveSiteFeatures=function(siteFeatures){
        return serviceHandler.executePostService("/SecurityApi/SaveSiteFeatures", siteFeatures);
    }
    
    this.getUsers=function(){
        return serviceHandler.executeGetService("/SecurityApi/GetUsers/" + headerController().currentSiteId);
    }
    
    this.getUser=function(userId){
        return serviceHandler.executeGetService("/SecurityApi/GetUser/" + headerController().currentSiteId + "/" + userId);
    }

    this.deleteUser=function(userId){
        return serviceHandler.executeGetService("/SecurityApi/DeleteUser/" + headerController().currentSiteId + "/" + userId);
    }

    this.saveUser=function(user){
        return serviceHandler.executePostService("/SecurityApi/SaveUser/" + headerController().currentSiteId,user);
    }

    this.resetPassword=function(username){
        return serviceHandler.executeGetService("/SecurityApi/ResetPassword/" +  username);
    }

    this.resetUserPassword = function (userId) {
        return serviceHandler.executeGetService("/SecurityApi/ResetUserPassword/" + userId);
    }

    this.unlockUser = function (userId) {
        return serviceHandler.executeGetService("/SecurityApi/UnlockUser/" + userId);
    }

    this.confirmUser = function (userId) {
        return serviceHandler.executeGetService("/SecurityApi/ConfirmUser/" + userId);
    }

    this.approveUser = function (userId) {
        return serviceHandler.executeGetService("/SecurityApi/ApproveUser/" + userId);
    }

    this.changePassword = function (changePassword) {
        return serviceHandler.executePostService("/SecurityApi/ChangePassword/" ,changePassword);
    }

    this.getNavigations=function(){
        return serviceHandler.executeGetService("/SecurityApi/GetNavigations/" + headerController().currentSiteId);
    }

    this.getNavigation=function(navigationId){
        return serviceHandler.executeGetService("/SecurityApi/GetNavigation/" + headerController().currentSiteId+"/"+navigationId);
    }

    this.deleteNavigation=function(navigationId){
        return serviceHandler.executeGetService("/SecurityApi/DeleteNavigation/" + headerController().currentSiteId+"/"+navigationId);
    }
    
    this.saveNavigation=function(navigation){
        return serviceHandler.executePostService("/SecurityApi/SaveNavigation/" + headerController().currentSiteId, navigation);
    }
     
    this.getUserNavigations=function(){
        return serviceHandler.executeGetService("/SecurityApi/GetUserNavigations/" + headerController().currentSiteId);
    }
     
    this.getNavigationTypes=function(){
        return serviceHandler.executeGetService("/SecurityApi/GetNavigationTypes");
    }

    //controller methods
    this.switchSite=function(siteId){
        return serviceHandler.executeGetService("/Security/SwitchSite/"+siteId);
    }

    this.getNavigationView = function (navigationId) {
        return serviceHandler.executeGetService("/Security/Navigation/" + navigationId);
    }

    this.getRoleView = function (roleId) {
        return serviceHandler.executeGetService("/Security/Role/" + roleId);
    }

    this.getRoleFeaturesView=function(roleId){
        return serviceHandler.executeGetService("/Security/RoleFeatures/" + roleId); 
    }

    this.getSiteView = function (siteId) {
        return serviceHandler.executeGetService("/Security/Site/" + siteId);
    }

    this.getSiteFeaturesView = function (siteId) {
        return serviceHandler.executeGetService("/Security/SiteFeatures/" + siteId);
    }

    this.getUserView = function (userId) {
        return serviceHandler.executeGetService("/Security/ManageUser/" + userId);
    }
 
}]);
