
    add-migration InitMysqlDB -Context PageTechsLMSDbContext -o Data/Mysql/Migrations 

    update-database -Context PageTechsLMSDbContext  -Migration InitMysqlDB