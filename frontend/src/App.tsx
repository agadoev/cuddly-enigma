import './App.css';

type WishViewModel = {
  id: number,
  title: string,
  description: string,
  price: number
};

type WishCardProps = {
  wish: WishViewModel,
  index: number
}

const WishCard= (props: WishCardProps) => {
  return (
    <div key={props.wish.id} style={{ fontSize: '2rem' }}>
      <span style={{color: "white", minWidth: '10px'}}>{props.index}</span>
      <span className='text' style={{ marginLeft: '20px' }}>{props.wish.title}</span>
    </div>
  )
    
}

function App() {

  const wishes: WishViewModel[] = [
    {
      "id": 683223,
      "title": "dolore",
      "description": "deserunt deserunt excepteur nisi ipsum ad ex laboris ut elit",
      "price": 6756
    },
    {
      "id": 713554,
      "title": "cillum",
      "description": "cupidatat nostrud deserunt Lorem nulla id ea occaecat ea qui",
      "price": 8001
    },
    {
      "id": 797627,
      "title": "laborum",
      "description": "fugiat proident non esse aliquip proident laboris proident eiusmod id",
      "price": 2729
    },
    {
      "id": 549943,
      "title": "officia",
      "description": "reprehenderit nostrud labore do qui amet nisi occaecat occaecat enim",
      "price": 5366
    },
    {
      "id": 276157,
      "title": "aute",
      "description": "cupidatat id ut aute aute ipsum sint pariatur eu duis",
      "price": 2069
    },
    {
      "id": 16400,
      "title": "commodo",
      "description": "nostrud dolor id ad consequat consectetur proident culpa enim ipsum",
      "price": 9492
    },
    {
      "id": 540950,
      "title": "nostrud",
      "description": "ex excepteur excepteur eiusmod eiusmod exercitation qui mollit reprehenderit id",
      "price": 7007
    }
  ]

  return (
    <div className="wrapper">

      <input type="text" />

      <div>29</div>


      <ul>
      {
        wishes.map((wish: WishViewModel, i: number) => <li><WishCard key={wish.id} index={i} wish={wish} /></li>)
      }
      </ul>
      <button>add wish</button>
    </div>
  )
}

export default App;
