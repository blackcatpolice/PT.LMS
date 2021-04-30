import React, { useEffect, useState } from 'react'

import DynamicForm from '@/components/DynamicForm/DynamicForm'
import { ModelType } from '@/components/DynamicForm/DynamicForm.d.ts'
import { UserClient } from '@/apis/API'
const userClient = new UserClient();
const registerForm: React.FC<{}> = (props) => {
    const [modelTypes, setModelTypes] = useState<ModelType[]>([]);

    useEffect(()=>{
        
    },[])

    return (
        <DynamicForm
            onSubmit={(values) => {

            }}
            modelTypes={modelTypes}
        >
        </DynamicForm>
    )
}

export default registerForm