export interface QuestionPropType{
    type:QuestionType,
    questionContent:string,
    options:string[]
}

export enum QuestionType{
    
    Single,
    Multi,
    Write
}