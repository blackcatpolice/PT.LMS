import React from 'react'
import {Radio,Checkbox} from 'antd'
import {QuestionPropType,QuestionType} from './Question.d';
import {AnswerPropType} from './Answer'
import {AnswerResultPropType} from './AnswerResult'

const QuizComponent : React.FC<{questions:QuestionPropType[],answer:AnswerPropType[],AnswerResult:AnswerResultPropType}>=(props)=>{
    const {questions,answer,AnswerResult} = props
    const createQuestion=(optItem:QuestionPropType)=>{
        switch (optItem.type) {
            case QuestionType.Write:
                return (
                    <div dangerouslySetInnerHTML={{__html:"<div></div>"}} />
                )
            case QuestionType.Multi:
            function onChange(checkedValues:any) {
                console.log('checked = ', checkedValues);
              }
            const options = [
                { label: 'Apple', value: 'Apple' },
                { label: 'Pear', value: 'Pear' },
                { label: 'Orange', value: 'Orange' },
              ];
                return (<Checkbox.Group options={options} defaultValue={['Apple']} onChange={onChange} />)
            case QuestionType.Single: 
            default:
            const optionsRadio = [
                { label: 'Apple', value: 'Apple' },
                { label: 'Pear', value: 'Pear' },
                { label: 'Orange', value: 'Orange' },
              ];
              const   value="";
                return ( 
                    <Radio.Group
                    options={optionsRadio}
                    onChange={()=>{}}
                    value={value}
                    optionType="button"
                  />)
        }
    }
    return (<div></div>)
}

export default QuizComponent;