# PT.LMS

A project that build  with .net 5 ,  identityserver4 ,Typescript ,Reac , Antd Design from pagetechs.com.

A Learning Management System


## Diretory

### PagetechsLMS.WebAndApi 

The front-business. Show for customer contents and payment. People can pay for content with this project.

This project is build from .Net 5. It has identityserverjwt , so it supports login with jwt and login whit form. 

For identityserverjwt is a sapmle version of identityserver4. You can config it by appsetting.config.And the simplest config is 

    "IdentityServer": {
      "Key": {
        "Type": "Development"
      } ,
      "Clients": {
        "PagetechsLMS.WebAndApiAPI": {
          "Profile": "IdentityServerSPA"
        }
      }
    }
   
If you use identityserverjwt and run error. You can check this config.

### PageTechsLMS.Service

For setting , it support tow methods load data, first from set.config.json , second from db. You can load data from set.config.json like wxconfig wxpay config ,
cdn bucket config . And you can set data like site name , site keywords , site descript , and display on fornt-business project PTLMS.WebAndApi.

Now , this service layer include  Courses,Pay, Orders code. and the other business code is writing. And payment now is only use wxpay. and the login is support wxlogin.


### PageTechsLMS.DataCore

This project include db code first ,and the docments is on ./document directory.

### Pagetechs.Framework

This project include some basic methods. It likes a utitlity project, now it only include littel codes.

### PageTechsLMS.Admin

This project is build with .Net 5 , Antd React for admin panel, Identityserver4 for auth. 

You can config from clien.config for identityserver. 
It supports switch diffrent db like sqlite , mysql , mssql, but you need migration before you run.

In ClientApp antd react , it supports dynamic form element build. The build type /pagetechs-lms-opensource/Pagetechs.Framework/Dtos/ModolHelper/ControlType.cs
When you create db with code first , you can use [ModelType] attribute to special how to render form element.

For apis , this project support swagger to generate swagger api json , and it also include NSwag package to generate client api.ts with axios. So for this api.ts, 
front-end code must use typescript version.


## Tasks

Now, this project has basic admin panel, badic website and webapi and basic simple pay for content. And I don't know what will do for this project. So I want more people 
can help me , you can make a damend for me or discussion together. 

You can send me email master@pagetechs.com and you can add my wechat friend : geek-master

Wellcom contact me.

