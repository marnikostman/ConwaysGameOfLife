class Grid
  def initialize
    @baseGrid = Array.new(20){Array.new(20){0}}
    r = Random.new
    (0..19).each do |i|
      (0..19).each do |j|
        if r.rand(0..2) == 0
          @baseGrid[i][j] = 1
        end
      end
    end
  end

  def withinRange (num)
    if num < 0 || num > 19
      return false
    else
      return true
    end
  end

  def count_neighbors (x, y)
    neighbors = 0
    xArray = [-1, 0, 1, 1, 1, 0, -1, -1]
    yArray = [-1, -1, -1, 0, 1, 1, 1, 0]
    (0..7).each do |i|
      if withinRange(x + xArray[i]) && withinRange(y + yArray[i])
        if @baseGrid[x + xArray[i]][y + yArray[i]] == 1
          neighbors += 1
        end
      end
    end
    return neighbors
  end

  def apply_rules()
    changeGrid = Array.new(20){Array.new(20){0}}
    (0..19).each do |i|
      (0..19).each do |j|
        if @baseGrid[i][j] == 1
          if count_neighbors(i, j) > 3 || count_neighbors(i, j) < 2
            changeGrid[i][j] = 0
          else
            changeGrid[i][j] = 1
          end
        else
          if count_neighbors(i, j) == 3
            changeGrid[i][j] = 1
          else
            changeGrid[i][j] = 0
          end
        end
      end
    end
    @baseGrid = Marshal.load(Marshal.dump(changeGrid))
  end

  def drawGrid()
    (0..19).each do |i|
      (0..19).each do |j|
        print @baseGrid[i][j]
      end
    print "\n"
    end
  end
end

grid = Grid.new
grid.drawGrid
while true
  system "clear"
  grid.apply_rules
  grid.drawGrid
  sleep(1)
end
