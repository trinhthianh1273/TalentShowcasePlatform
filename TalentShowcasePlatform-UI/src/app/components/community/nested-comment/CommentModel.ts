
export interface CommentModel {
  id: string;
  groupPostId : string,
  userId : string,
  userName: string;
  userAvatar: string;
  content: string;
  createdAt : Date;
  parentCommentId?: string | null;
  children?: CommentModel[];
}

export const commentsMockData: CommentModel[] = [
  {
    id: '1',
    userName: 'lymtics0502',
    userAvatar: 'https://storage.googleapis.com/a1aa/image/361f4066-399e-42f0-3e1f-f6677e9c9276.jpg',
    content: `Trường hợp rơi vào lỗi biện luận red herring - biện luận cá trích...`,
    timeDiff: '4d ago',
    vote: 79,
    children: [
      {
        id: '1-1',
        userName: 'Responsible-Space-41',
        userAvatar: 'https://storage.googleapis.com/a1aa/image/361f4066-399e-42f0-3e1f-f6677e9c9276.jpg',
        content: `Cái này công kích cá nhân thì là ad hominem chứ`,
        timeDiff: '3d ago',
        vote: 15,
        children: [
          {
            id: '1-1-1',
            userName: 'lymtics0502',
            userAvatar: 'https://storage.googleapis.com/a1aa/image/361f4066-399e-42f0-3e1f-f6677e9c9276.jpg',
            content: `Chính xác, ad hominem là đúng nhất nếu dùng để trả lời cho chủ đề của OP.`,
            timeDiff: '3d ago',
            vote: 6,
            children: [
              {
                id: '1-1-1-1',
                userName: 'sonething33',
                userAvatar: 'https://storage.googleapis.com/a1aa/image/03e80626-d313-4e0e-bc06-f5b6663235b7.jpg',
                content: `Sao bro nhắn như dùng chatGPT v :))`,
                timeDiff: '3d ago',
                vote: 12,
                children: [
                  {
                    id: '1-1-1-1-1',
                    userName: 'lymtics0502',
                    userAvatar: 'https://storage.googleapis.com/a1aa/image/361f4066-399e-42f0-3e1f-f6677e9c9276.jpg',
                    content: `Xác nhận là chính chủ nhắn nha thím. Sai thì nhận và sửa chữa thôi.`,
                    timeDiff: '3d ago',
                    vote: 1,
                    children: [
                      {
                        id: '1-1-1-1-1-1',
                        userName: 'AUnHIALoopHT',
                        userAvatar: 'https://storage.googleapis.com/a1aa/image/868a8534-4fde-404d-11e5-728ab5ddc091.jpg',
                        content: `Cm nhìn giống thật lmao`,
                        timeDiff: '3d ago',
                        vote: 0,
                        children: []
                      },
                      {
                        id: '1-1-1-1-1-1',
                        userName: 'AUnHIALoopHT',
                        userAvatar: 'https://storage.googleapis.com/a1aa/image/868a8534-4fde-404d-11e5-728ab5ddc091.jpg',
                        content: `Cm nhìn giống thật lmao`,
                        timeDiff: '3d ago',
                        vote: 0,
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
    userName: 'lymtics0502',
    userAvatar: 'https://storage.googleapis.com/a1aa/image/361f4066-399e-42f0-3e1f-f6677e9c9276.jpg',
    content: `Trường hợp rơi vào lỗi biện luận red herring - biện luận cá trích...`,
    timeDiff: '4d ago',
    vote: 79,
    children: [
      {
        id: '1-1',
        userName: 'Responsible-Space-41',
        userAvatar: 'https://storage.googleapis.com/a1aa/image/361f4066-399e-42f0-3e1f-f6677e9c9276.jpg',
        content: `Cái này công kích cá nhân thì là ad hominem chứ`,
        timeDiff: '3d ago',
        vote: 15,
        children: [
          {
            id: '1-1-1',
            userName: 'lymtics0502',
            userAvatar: 'https://storage.googleapis.com/a1aa/image/361f4066-399e-42f0-3e1f-f6677e9c9276.jpg',
            content: `Chính xác, ad hominem là đúng nhất nếu dùng để trả lời cho chủ đề của OP.`,
            timeDiff: '3d ago',
            vote: 6,
            children: [
              {
                id: '1-1-1-1',
                userName: 'sonething33',
                userAvatar: 'https://storage.googleapis.com/a1aa/image/03e80626-d313-4e0e-bc06-f5b6663235b7.jpg',
                content: `Sao bro nhắn như dùng chatGPT v :))`,
                timeDiff: '3d ago',
                vote: 12,
                children: [
                  {
                    id: '1-1-1-1-1',
                    userName: 'lymtics0502',
                    userAvatar: 'https://storage.googleapis.com/a1aa/image/361f4066-399e-42f0-3e1f-f6677e9c9276.jpg',
                    content: `Xác nhận là chính chủ nhắn nha thím. Sai thì nhận và sửa chữa thôi.`,
                    timeDiff: '3d ago',
                    vote: 1,
                    children: [
                      {
                        id: '1-1-1-1-1-1',
                        userName: 'AUnHIALoopHT',
                        userAvatar: 'https://storage.googleapis.com/a1aa/image/868a8534-4fde-404d-11e5-728ab5ddc091.jpg',
                        content: `Cm nhìn giống thật lmao`,
                        timeDiff: '3d ago',
                        vote: 0,
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
  
];
