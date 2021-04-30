import React from 'react';
import {Modal,Form ,Input,Button,Radio} from 'antd';
import { UploadOutlined ,MinusCircleOutlined,PlusOutlined} from '@ant-design/icons';

import 'braft-editor/dist/index.css' 
import BraftEditorWrapper   from '../BraftEditor/BraftEditorWrapper'
import {QuestionPropType,QuestionType} from './Question.d';

const formItemLayout = {
    labelCol: {
      xs: { span: 24 },
      sm: { span: 4 },
    },
    wrapperCol: {
      xs: { span: 24 },
      sm: { span: 20 },
    },
  };
  const formItemLayoutWithOutLabel = {
    wrapperCol: {
      xs: { span: 24, offset: 0 },
      sm: { span: 20, offset: 4 },
    },
  };
const CreateQuizComponent :React.FC<{}>=(props)=>{
    const createQustion = ()=>{
         return (<>
            <Form.Item label="题目类型">
                    <Radio.Group
                options={options}
                onChange={this.onChange3}
                value={value3}
                optionType="button"
                />
            </Form.Item>
            <Form.Item>
                <></>
                <BraftEditorWrapper></BraftEditorWrapper>
            </Form.Item>
            
            <Form.List name="names"  >
        {(fields, { add, remove }) => {
          return (
            <div>
              {fields.map((field, index) => (
                <Form.Item
                  {...(index === 0 ? formItemLayout : formItemLayoutWithOutLabel)}
                  label={index === 0 ? 'Passengers' : ''}
                  required={false}
                  key={field.key}
                >
                  <Form.Item
                    {...field}
                    
                    rules={[
                      {
                        required: true,
                        whitespace: true,
                        message: "Please input passenger's name or delete this field.",
                      },
                    ]}
                    noStyle
                  >
                    <Input placeholder="passenger name" style={{ width: '60%' }} />
                  </Form.Item>
                  {fields.length > 1 ? (
                    <MinusCircleOutlined
                      className="dynamic-delete-button"
                      style={{ margin: '0 8px' }}
                      onClick={() => {
                        remove(field.name);
                      }}
                    />
                  ) : null}
                </Form.Item>
              ))}
              <Form.Item>
                <Button
                  type="dashed"
                  onClick={() => {
                    add();
                  }}
                  style={{ width: '60%' }}
                >
                  <PlusOutlined /> Add field
                </Button>
                <Button
                  type="dashed"
                  onClick={() => {
                    add('The head item', 0);
                  }}
                  style={{ width: '60%', marginTop: '20px' }}
                >
                  <PlusOutlined /> Add field at head
                </Button>
              </Form.Item>
            </div>
          );
        }}
      </Form.List>
         </>)
    }
    return (<Modal></Modal>)    
}

export default CreateQuizComponent;