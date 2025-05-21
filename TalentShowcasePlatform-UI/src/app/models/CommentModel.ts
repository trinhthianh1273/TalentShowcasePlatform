
export interface CommentModel {
  id: string;
  groupPostId: string,
  userId: string,
  userName: string;
  userImgUrl: string;
  content: string;
  createdAt: Date;
  parentCommentId?: string | null;
  children?: CommentModel[];
}

export const commentsMockData: CommentModel[] = [
  {
    id: '1',
    groupPostId: '1-1-1-1-1',
        userId: 'AUnHIALoopHT',
    userName: 'lymtics0502',
    userImgUrl: 'https://storage.googleapis.com/a1aa/image/361f4066-399e-42f0-3e1f-f6677e9c9276.jpg',
    content: `Trường hợp rơi vào lỗi biện luận red herring - biện luận cá trích...`,
    createdAt: new Date(),
    children: [
      {
        id: '1-1',
        groupPostId: '1-1-1-1-1',
        userId: 'AUnHIALoopHT',
        userName: 'Responsible-Space-41',
        userImgUrl: 'https://storage.googleapis.com/a1aa/image/361f4066-399e-42f0-3e1f-f6677e9c9276.jpg',
        content: `Cái này công kích cá nhân thì là ad hominem chứ`,
        createdAt: new Date(),
        children: [
          {
            id: '1-1-1',
            groupPostId: '1-1-1-1-1',
            userId: 'AUnHIALoopHT',
            userName: 'lymtics0502',
            userImgUrl: 'https://storage.googleapis.com/a1aa/image/361f4066-399e-42f0-3e1f-f6677e9c9276.jpg',
            content: `Chính xác, ad hominem là đúng nhất nếu dùng để trả lời cho chủ đề của OP.`,
            createdAt: new Date(),
            children: [
              {
                id: '1-1-1-1',
                groupPostId: '1-1-1-1-1',
                userId: 'AUnHIALoopHT',
                userName: 'sonething33',
                userImgUrl: 'https://storage.googleapis.com/a1aa/image/03e80626-d313-4e0e-bc06-f5b6663235b7.jpg',
                content: `Sao bro nhắn như dùng chatGPT v :))`,
                createdAt: new Date(),
                children: [
                  {
                    id: '1-1-1-1-1',
                    groupPostId: '1-1-1-1-1',
                    userId: 'AUnHIALoopHT',
                    userName: 'lymtics0502',
                    userImgUrl: 'https://storage.googleapis.com/a1aa/image/361f4066-399e-42f0-3e1f-f6677e9c9276.jpg',
                    content: `Xác nhận là chính chủ nhắn nha thím. Sai thì nhận và sửa chữa thôi.`,
                    createdAt: new Date(),
                    children: [
                      {
                        id: '1-1-1-1-1-1',
                        groupPostId: '1-1-1-1-1',
                        userId: 'AUnHIALoopHT',
                        userName: 'AUnHIALoopHT',
                        userImgUrl: 'https://storage.googleapis.com/a1aa/image/868a8534-4fde-404d-11e5-728ab5ddc091.jpg',
                        content: `Cm nhìn giống thật lmao`,
                        createdAt: new Date(),
                        parentCommentId: '1-1-1-1-1',
                        children: []
                      }
                    ]
                  }
                ]
              }
            ]
          }
        ]
      }
    ]
  },
  {
    id: '1',
    groupPostId: '1-1-1-1-1',
        userId: 'AUnHIALoopHT',
    userName: 'lymtics0502',
    userImgUrl: 'https://storage.googleapis.com/a1aa/image/361f4066-399e-42f0-3e1f-f6677e9c9276.jpg',
    content: `Trường hợp rơi vào lỗi biện luận red herring - biện luận cá trích...`,
    createdAt: new Date(),
    children: [
      {
        id: '1-1',
        groupPostId: '1-1-1-1-1',
        userId: 'AUnHIALoopHT',
        userName: 'Responsible-Space-41',
        userImgUrl: 'https://storage.googleapis.com/a1aa/image/361f4066-399e-42f0-3e1f-f6677e9c9276.jpg',
        content: `Cái này công kích cá nhân thì là ad hominem chứ`,
        createdAt: new Date(),
        children: [
          {
            id: '1-1-1',
            groupPostId: '1-1-1-1-1',
            userId: 'AUnHIALoopHT',
            userName: 'lymtics0502',
            userImgUrl: 'https://storage.googleapis.com/a1aa/image/361f4066-399e-42f0-3e1f-f6677e9c9276.jpg',
            content: `Chính xác, ad hominem là đúng nhất nếu dùng để trả lời cho chủ đề của OP.`,
            createdAt: new Date(),
            children: [
              {
                id: '1-1-1-1',
                groupPostId: '1-1-1-1-1',
                userId: 'AUnHIALoopHT',
                userName: 'sonething33',
                userImgUrl: 'https://storage.googleapis.com/a1aa/image/03e80626-d313-4e0e-bc06-f5b6663235b7.jpg',
                content: `Sao bro nhắn như dùng chatGPT v :))`,
                createdAt: new Date(),
                children: [
                  {
                    id: '1-1-1-1-1',
                    groupPostId: '1-1-1-1-1',
                    userId: 'AUnHIALoopHT',
                    userName: 'lymtics0502',
                    userImgUrl: 'https://storage.googleapis.com/a1aa/image/361f4066-399e-42f0-3e1f-f6677e9c9276.jpg',
                    content: `Xác nhận là chính chủ nhắn nha thím. Sai thì nhận và sửa chữa thôi.`,
                    createdAt: new Date(),
                    children: [
                      {
                        id: '1-1-1-1-1-1',
                        groupPostId: '1-1-1-1-1',
                        userId: 'AUnHIALoopHT',
                        userName: 'AUnHIALoopHT',
                        userImgUrl: 'https://storage.googleapis.com/a1aa/image/868a8534-4fde-404d-11e5-728ab5ddc091.jpg',
                        content: `Cm nhìn giống thật lmao`,
                        createdAt: new Date(),
                        parentCommentId: '1-1-1-1-1',
                        children: []
                      }
                    ]
                  }
                ]
              }
            ]
          }
        ]
      }
    ]
  }

];
