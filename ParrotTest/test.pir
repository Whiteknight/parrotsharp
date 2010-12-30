.sub main :main
  say "Hello World from PIR!"
  $P0 = newclass['ClassTest']
  $P1 = new[$P0]
  $P1.'test'()
  .end

.namespace['ClassTest']
.sub test :method
  say "Hello World from method!"
  .end

.sub test2 :method
  say "Hello World from a method called with ParrotSharp!"
  .end
