
export type Task = {
    id:          number,
    name:        string,
    description: string,
}

export type TaskInput = {
    name:        string,
    description: string,
}

export const blank_task = (): TaskInput => ({
  name:        "",
  description: "",
});
