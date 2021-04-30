import React, { useState, useEffect } from 'react';
import { PageContainer } from '@ant-design/pro-layout'
import DynamicForm from '@/components/DynamicForm/DynamicForm';
import { DTOCategoryInput, Client } from '@/apis/API'
import { ModelType } from '@/components/DynamicForm/DynamicForm.d';
import { values } from 'lodash';

const info: React.FC<{}> = ({ }) => {
    var userInfoClient = new Client();
    const [modelInfo, setModelInfo] = useState<{ data?: any, modelType: ModelType[] }>()
    const [showform, setShowForm] = useState<boolean>(false);

    useEffect(() => {
        handleGetAddOrUpdate()
    }, [])

    const handleGetAddOrUpdate = async () => { 
        var res = await userInfoClient.getUserInfo();
        console.log('get info')
        setModelInfo({ data: res.data?.modelData, modelType: res.data?.modelType })
        setShowForm(true)
    }

    const handlSubmit = async (userInfo: any) => {
        //await userInfoClient.setUserInfo(userInfo)
    }

    return (<PageContainer>
        { showform && (<DynamicForm
            useModal={false}
            data={modelInfo?.data}
            modelTypes={modelInfo?.modelType || []}
            modelData={modelInfo?.data}
            onSubmit={async values => {
                // var viewModel = new UserInfoViewModel();
                // viewModel.avatar = values['avatar'];
                await handlSubmit(values)
            }}></DynamicForm>)}
    </PageContainer>)
}

export default info;